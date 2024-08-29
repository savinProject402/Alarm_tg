using System.Text.Json.Serialization;

namespace Alarm_TG;

public class AlertSystem
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
    /// Назва регіону
    /// </summary>
    public string RegionName { get; set; }

    /// <summary>
    /// Англійська назва регіону
    /// </summary>
    public string RegionEngName { get; set; }

    /// <summary>
    /// Час останнього оновлення
    /// </summary>
    public DateTime LastUpdate { get; set; }

    /// <summary>
    /// Активні тривоги
    /// </summary>
    public List<Alert> ActiveAlerts { get; set; }
}