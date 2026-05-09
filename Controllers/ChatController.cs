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

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessageRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Message))
            return BadRequest(new { reply = "Message cannot be empty." });

        var reply = await _aiService.GetReplyAsync(request.Message);
        return Json(new { reply });
    }
}
