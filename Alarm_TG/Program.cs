


using Alarm_TG;

using Alarm_TG.Repository;
using Microsoft.Extensions.Configuration;


var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ConfigureFile.json", optional: true, reloadOnChange: true);

IConfiguration configuration = builder.Build();

Alarm alarm = new Alarm(configuration);
await alarm.PostInfoByTelegram();

