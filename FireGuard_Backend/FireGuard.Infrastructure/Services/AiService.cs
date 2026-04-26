using System.Text;
using System.Text.Json;
using FireGuard.Application.Interfaces;

public class AiService : IAiService
{
    private readonly HttpClient _http;

    public AiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GetSummary(string prompt)
    {
        var req = new
        {
            model = "llama3",
            prompt = prompt,
            stream = false
        };

        var json = JsonSerializer.Serialize(req);

        var res = await _http.PostAsync(
            "http://localhost:11434/api/generate",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var text = await res.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(text);

        return doc.RootElement
            .GetProperty("response")
            .GetString() ?? "";
    }
}