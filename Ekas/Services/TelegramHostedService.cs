using Ekas.Common;
using Ekas.Monitoring.Handlers;
using Ekas.Monitoring.Repository;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Radzen;

namespace Ekas.Monitoring.Services
{
    public class TelegramHostedService: BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public TelegramHostedService(IServiceProvider serviceProvider)
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
            var updateHandler = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UpdateHandler>();
            var botClient = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ITelegramBotClient>();
            using CancellationTokenSource cts = new();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            botClient.StartReceiving(
                updateHandler: updateHandler.HandleUpdateAsync,
                pollingErrorHandler: updateHandler.HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token);

        }
    }
}
