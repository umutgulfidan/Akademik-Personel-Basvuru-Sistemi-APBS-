using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WebApi.Hubs
{
    public class NotificationHub : Hub
    {
        private static Dictionary<string, string> _userConnections = new Dictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            // Kullanıcı ID'sini al
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                lock (_userConnections)
                {
                    _userConnections[userId] = Context.ConnectionId;
                }
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                lock (_userConnections)
                {
                    _userConnections.Remove(userId);
                }
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendPrivateMessage(string userId, string message)
        {
            if (_userConnections.TryGetValue(userId, out string connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
            }
        }
    }
}
