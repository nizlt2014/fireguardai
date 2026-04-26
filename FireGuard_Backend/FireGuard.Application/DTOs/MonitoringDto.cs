namespace FireGuard.Application.DTOs;

public class MonitoringDto
{
    public int OnlineDevices { get; set; }
    public int Warnings { get; set; }
    public int Critical { get; set; }
    public int SitesConnected { get; set; }

    public List<DeviceDto> Devices { get; set; } = new();
}

public class DeviceDto
{
    public int Id { get; set; }
    public string SiteName { get; set; } = "";
    public string DeviceName { get; set; } = "";
    public string DeviceType { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime LastSeen { get; set; }
    public int BatteryLevel { get; set; }
}