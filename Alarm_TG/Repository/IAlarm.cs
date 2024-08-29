namespace Alarm_TG.Repository;

public interface IAlarm
{
    Task<List<AlertSystem>> GetByRegionId();
    Task PostInfoByTelegram();
}