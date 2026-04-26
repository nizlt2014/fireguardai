namespace FireGuard.Application.Interfaces;

public interface IAiService
{
    Task<string> GetSummary(string prompt);
}