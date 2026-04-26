namespace FireGuard.Application.DTOs;

public class TechniciansDto
{
    public int TotalTechnicians { get; set; }
    public int ActiveJobs { get; set; }
    public int AvailableNow { get; set; }
    public int AvgEta { get; set; }

    public List<TechnicianItemDto> Technicians { get; set; } = new();
}

public class TechnicianItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Status { get; set; } = "";
    public string SiteName { get; set; } = "";
    public int ETAMinutes { get; set; }
    public int JobsToday { get; set; }
    public string Skill { get; set; } = "";
}