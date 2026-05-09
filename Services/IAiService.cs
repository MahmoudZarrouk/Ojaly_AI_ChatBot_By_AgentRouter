namespace OjalyChatBot.Services;

public interface IAiService
{
    Task<string> GetReplyAsync(string userMessage);
}
