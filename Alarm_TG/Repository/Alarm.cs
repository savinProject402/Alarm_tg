using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Alarm_TG.Repository;

public class Alarm : IAlarm
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private string regionId;
    private string alarmMessage;
    private string alarmMessageResolve;
    private string pathAlarm;
    private string pathResolve;
    private Telegram _telegram;
    private static bool isAlarmActive = false;

    public Alarm(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _configuration = configuration;
        regionId = _configuration["ApiSettings:StateId"];
        alarmMessage = _configuration["ApiSettings:messageAlarm"];
        alarmMessageResolve = _configuration["ApiSettings:messageAlarmResolve"];
        pathAlarm = _configuration["ApiSettings:pathAlarm"];
        pathResolve = _configuration["ApiSettings:pathAlarmResolve"];
        _telegram = new Telegram(configuration);
    }
    
    public async Task<List<AlertSystem>> GetByRegionId()
    {
        string url = $"https://api.ukrainealarm.com/api/v3/alerts/{regionId}";
        
        string token = _configuration["ApiSettings:TokenAjax"];
        
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        request.Headers.TryAddWithoutValidation("Authorization", token);

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var alertSystemJson = await response.Content.ReadAsStringAsync();
            var alertSystem = JsonConvert.DeserializeObject<List<AlertSystem>>(alertSystemJson);
            return alertSystem;
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode} in method GetByRegionId");
            return null;
        }
    }

    public async Task PostInfoByTelegram()
    {
        try
        {
            while (true)
            {
                var alert = await GetByRegionId();

                if (alert != null && alert[0].ActiveAlerts.Count == 1 && !isAlarmActive)
                {
                    isAlarmActive = true;
                    await _telegram.SendPhotoWithText(_telegram.ChatId, pathAlarm, alarmMessage);
                }
                else if ((alert == null || alert[0].ActiveAlerts.Count == 0) && isAlarmActive)
                {
                    isAlarmActive = false;
                    await _telegram.SendPhotoWithText(_telegram.ChatId, pathResolve, alarmMessageResolve);
                }
                
                await Task.Delay(TimeSpan.FromSeconds(90));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}