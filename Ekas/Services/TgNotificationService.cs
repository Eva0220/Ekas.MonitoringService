using Telegram.Bot;

namespace Ekas.Monitoring.Services
{
    public class TgNotificationService
    {
        private readonly ITelegramBotClient _botClient;
        public TgNotificationService(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        public async Task SendMessageToTelegramAsync(long chatId, string message)
        {
            try
            {
                await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: message
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message to Telegram: {ex.Message}");
            }
        }
    }
}
