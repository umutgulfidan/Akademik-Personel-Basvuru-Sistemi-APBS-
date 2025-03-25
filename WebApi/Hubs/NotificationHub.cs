using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;
using Core.Extensions.Claims;

namespace WebApi.Hubs
{
    public class NotificationHub : Hub
    {
        // Bağlantıları saklamak için Dictionary kullanıyoruz
        private static readonly ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();

        // Kullanıcı bağlantı sağladığında
        public override async Task OnConnectedAsync()
        {
            int userId = 0;
            if (Context.User.Identity.IsAuthenticated)
            {
                // Kullanıcı kimliğini Context üzerinden alıyoruz
                userId = Context.User.ClaimUserId(); // ClaimsPrincipal üzerinden ClaimUserId metodunu çağırıyoruz

            }

            if (userId != 0)
            {
                // Kullanıcı bağlantı ID'sini kaydet
                _userConnections[Convert.ToString(userId)] = Context.ConnectionId;
                Console.WriteLine($"Kullanıcı {userId} bağlantı kurdu: {Context.ConnectionId}");
            }
            else
            {
                Console.WriteLine("Kullanıcı kimliği alınamadı.");
            }

            await base.OnConnectedAsync();
        }

        // Bağlantı kesildiğinde, kullanıcıyı bağlantı listesinde temizle
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                // Kullanıcıyı bağlantı listesinde kaldır
                _userConnections.TryRemove(userId, out _);
            }

            return base.OnDisconnectedAsync(exception);
        }

        // Kullanıcı bağlantı ID'sini almak için bir statik metot
        public static string GetConnectionId(string userId)
        {
            _userConnections.TryGetValue(userId, out string connectionId);
            return connectionId;
        }

        // Özel bildirim gönderme metodu
        public async Task SendPrivateMessage(string userId, string message)
        {
            // Kullanıcının bağlantı ID'sini al
            if (_userConnections.TryGetValue(userId, out string connectionId))
            {
                // Bağlantı varsa, kullanıcının bağlantısına mesaj gönder
                await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
            }
        }

        // Admin'in bildirim gönderebilmesi için metot
        public async Task SendNotification(string userId, string message)
        {
            // Kullanıcının bağlantı ID'sini al
            if (_userConnections.TryGetValue(userId, out string connectionId))
            {
                // Bağlantı varsa, kullanıcının bağlantısına bildirim gönder
                await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
            }
            else
            {
                // Bağlantı bulunamazsa hata mesajı gönder
                await Clients.Caller.SendAsync("ErrorNotification", "Kullanıcı bağlantısı bulunamadı.");
            }
        }
    }
}
