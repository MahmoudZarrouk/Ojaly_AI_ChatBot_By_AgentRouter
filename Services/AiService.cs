using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OjalyChatBot.Services;

public class AiService : IAiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> GetReplyAsync(string userMessage)
    {
        var apiKey = _configuration["Groq:ApiKey"];
        var baseUrl = _configuration["Groq:BaseUrl"];
        var model = _configuration["Groq:Model"];

        var requestBody = new
        {
            model = model,
            messages = new[]
            {
                new { role = "user", content = userMessage }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);

        using var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await _httpClient.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return $"API Error: {response.StatusCode} - URL Used: {baseUrl} - Response: {responseString}";
        }

        using var doc = JsonDocument.Parse(responseString);

        return doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "No response received.";
    }
}
