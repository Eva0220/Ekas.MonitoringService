using Confluent.Kafka;
using Ekas.Common;
using Ekas.Monitoring.Handlers;
using Ekas.Monitoring.Models;
using Ekas.Monitoring.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Radzen;
using System.Text.Json;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Ekas.Monitoring.Services
{
    public class KafkaHostedService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private KafkaConsumer kafkaConsumer;

        public KafkaHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            await Work(cancellationToken);
        }

        public async Task Work(CancellationToken cancellationToken)
        {
            kafkaConsumer = serviceProvider.GetRequiredService<KafkaConsumer>();
            var incidentRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IncidentRepository>();
            var userRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UserRepository>();
            var notificationService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TgNotificationService>();
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    kafkaConsumer.Subscribe(AppData.Topic);
                    var message = await kafkaConsumer.GetMessages();
                    if (message != null)
                    {
                        Incident incident = JsonSerializer.Deserialize<Incident>(message);
                        await incidentRepository.AddIncident(incident);
                        var chatIds = userRepository.GetChatIds();
                            foreach (var chatId in chatIds)
                            {
                                await notificationService.SendMessageToTelegramAsync(chatId, Texts.errorNotification);
                            }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                kafkaConsumer.Close();

            }
        }
    }
}
