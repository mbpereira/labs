using Microsoft.AspNetCore.SignalR;

namespace Lab.SignalR.WebApi;

public class ChatHub : Hub
{
    public async Task SendSignal(string user, string message)
    {
        // Método de callback do que o listener do front recebe
        await Clients.All.SendAsync("ReceiveSignal", user, message);
    }
}