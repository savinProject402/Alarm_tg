using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Alarm_TG.Repository;

public class Telegram : ITelegram
{
    private static TelegramBotClient botClient;
    private readonly IConfiguration _configuration;
    public string ChatId { get; set ; }
    
    public Telegram(IConfiguration configuration)
    {
        _configuration = configuration;
        botClient = new TelegramBotClient(_configuration["ApiSettings:TokenTG"]);
        ChatId = _configuration["ApiSettings:channelId"];
    }
    
    public async Task GetInfo()
    {

        var botInfo = await botClient.GetMeAsync();
        Console.WriteLine($"Bot id: {botInfo.Id} and Bot name: {botInfo.FirstName}");

    }

    public Task PostMessage()
    {
        throw new NotImplementedException();
    }

    public async Task SendMessageToChannel(string channelId, string messageText)
    {
        try
        {
            var message = await botClient.SendTextMessageAsync(
                chatId: channelId,
                text: messageText
            );
            Console.WriteLine($"Повідомлення відправлено: {message.MessageId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при відправці повідомлення: {ex.Message}");
        }
    }

    public async Task SendPhotoWithText(string channelId, string imagePath, string messageText)
    {
        try
        {
            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var inputOnlineFile = new InputFileStream(stream, Path.GetFileName(imagePath));
                
                await botClient.SendPhotoAsync(
                    chatId: channelId,
                    photo: inputOnlineFile,
                    caption: messageText
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при відправці повідомлення: {ex.Message}");
        }
    }
}