namespace Alarm_TG.Repository;

public interface ITelegram
{
    Task GetInfo();
    Task PostMessage();
    Task SendMessageToChannel(string channelId, string messageText);
    Task SendPhotoWithText(string channelId, string imagePath, string messageText);
}