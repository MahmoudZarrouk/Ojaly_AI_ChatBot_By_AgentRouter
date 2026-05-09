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
        var apiKey = _configuration["AgentRouter:ApiKey"];
        var baseUrl = _configuration["AgentRouter:BaseUrl"];
        var model = _configuration["AgentRouter:Model"];

        if (string.IsNullOrWhiteSpace(apiKey) || apiKey == "sk-BkAv7LIIeGEufDEWlKhMdTdSDliRz4LBjZlC5c0zIyaEo2oL")
            return "Please add your AgentRouter API key in appsettings.json first.";

        var requestBody = new
        {
            model = model,
            messages = new[] { new { role = "user", content = userMessage } }
        };

        var json = JsonSerializer.Serialize(requestBody);
        using var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await _httpClient.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return $"Sorry, I couldn't get a response right now. API Error: {response.StatusCode}";

        using var document = JsonDocument.Parse(responseText);

        return document.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "Sorry, I couldn't understand the response.";
    }
}
