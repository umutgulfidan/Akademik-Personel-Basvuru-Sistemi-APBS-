using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class SignalRHub : Hub
    {
        public async Task SendGlobalMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification",message);
        }
    }
}
