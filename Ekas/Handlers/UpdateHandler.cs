using Ekas.Monitoring.Services;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Ekas.Monitoring.Repository;

namespace Ekas.Monitoring.Handlers
{
    public class UpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly UserRepository _userRepository;

        public UpdateHandler(ITelegramBotClient botClient, UserRepository userRepository)
        {
            _botClient = botClient;
            _userRepository = userRepository;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;
            _userRepository.AddChatId(chatId);

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"{Texts.HelloMessage}",
                cancellationToken: cancellationToken);
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;

        }
    }
}

