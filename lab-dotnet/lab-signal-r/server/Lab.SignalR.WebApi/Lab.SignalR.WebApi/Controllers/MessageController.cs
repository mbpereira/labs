using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Lab.SignalR.WebApi.Controllers;

[ApiController]
[Route("/message")]
public class MessageController : ControllerBase
{
    private readonly IHubContext<ChatHub> _context;

    public MessageController(IHubContext<ChatHub> context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] MessageDTO message)
    {
        await _context.Clients.All.SendAsync("ReceiveSignal", message.From, message.Content);
        return Ok();
    }
}