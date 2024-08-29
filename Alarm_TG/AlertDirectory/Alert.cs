namespace Alarm_TG;

using System;

public class Alert
{
    /// <summary>
    /// Ідентифікатор регіону
    /// </summary>
    public string RegionId { get; set; }

    /// <summary>
    /// Тип регіону
    /// </summary>
    public V2RegionType RegionType { get; set; }

    /// <summary>
    /// Тип тривоги
    /// </summary>
    public AlertType Type { get; set; }

    /// <summary>
    /// Час останнього оновлення
    /// </summary>
    public DateTime LastUpdate { get; set; }
    
}