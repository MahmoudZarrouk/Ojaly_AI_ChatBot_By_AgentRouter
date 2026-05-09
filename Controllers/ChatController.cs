using Microsoft.AspNetCore.Mvc;
using OjalyChatBot.Models;
using OjalyChatBot.Services;

namespace OjalyChatBot.Controllers;

public class ChatController : Controller
{
    private readonly IAiService _aiService;

    public ChatController(IAiService aiService)
    {
        _aiService = aiService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("/api/chat/send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessageRequest request)
    {
        try
        {
            Console.WriteLine("===== API /api/chat/send was called =====");

            if (request == null)
            {
                return Ok(new { reply = "Backend Error: Request body is null." });
            }

            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return Ok(new { reply = "Backend Error: Message is empty." });
            }

            var reply = await _aiService.GetReplyAsync(request.Message);

            return Ok(new { reply });
        }
        catch (Exception ex)
        {
            return Ok(new
            {
                reply = "Backend Exception: " + ex.Message
            });
        }
    }
}