using Core.Extensions.Claims;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

public class NotificationHub : Hub
{
    // Bağlantıları saklamak için Dictionary kullanıyoruz
    private static readonly ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>(); // Authenticated users
    private static readonly ConcurrentDictionary<string, string> _unauthenticatedConnections = new ConcurrentDictionary<string, string>(); // Unauthenticated users

    // Kullanıcı bağlantı sağladığında
    public override async Task OnConnectedAsync()
    {
        int userId = 0;
        if (Context.User.Identity.IsAuthenticated)
        {
            // Kullanıcı kimliğini Context üzerinden alıyoruz
            userId = Context.User.ClaimUserId(); // ClaimsPrincipal üzerinden ClaimUserId metodunu çağırıyoruz

            if (userId != 0)
            {
                // Kullanıcı bağlantı ID'sini kaydet
                _userConnections[Convert.ToString(userId)] = Context.ConnectionId;
                Console.WriteLine($"Authenticated Kullanıcı {userId} bağlantı kurdu: {Context.ConnectionId}");
            }
        }
        else
        {
            // Authenticated olmayan kullanıcının bağlantı ID'sini kaydediyoruz
            _unauthenticatedConnections[Context.ConnectionId] = "Unauthenticated User";
            Console.WriteLine($"Unauthenticated kullanıcı bağlantı kurdu: {Context.ConnectionId}");
        }

        await base.OnConnectedAsync();
    }

    // Bağlantı kesildiğinde, kullanıcıyı bağlantı listesinde temizle
    public override Task OnDisconnectedAsync(Exception exception)
    {
        if (_userConnections.Values.Contains(Context.ConnectionId))
        {
            var userId = Context.User.ClaimUserId();
            if (userId != null)
            {
                // Giriş yapmış kullanıcıyı bağlantı listesinde kaldır
                _userConnections.TryRemove(Convert.ToString(userId), out _);
            }
        }
        else
        {
            // Giriş yapmamış kullanıcıyı bağlantı listesinde kaldır
            _unauthenticatedConnections.TryRemove(Context.ConnectionId, out _);
        }

        return base.OnDisconnectedAsync(exception);
    }

    // Giriş yapmayan kullanıcı sayısını almak için bir statik metot
    public static int GetUnauthenticatedUserCount()
    {
        return _unauthenticatedConnections.Count; // Giriş yapmayan kullanıcıların sayısını döndür
    }

    // Giriş yapmış kullanıcı sayısını almak için bir statik metot
    public static int GetAuthenticatedUserCount()
    {
        return _userConnections.Count; // Giriş yapmış kullanıcıların sayısını döndür
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
        if (_userConnections.TryGetValue(userId, out string connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
        }
    }

    // Admin'in bildirim gönderebilmesi için metot
    public async Task SendNotification(string userId, string message)
    {
        if (_userConnections.TryGetValue(userId, out string connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
        }
        else
        {
            await Clients.Caller.SendAsync("ErrorNotification", "Kullanıcı bağlantısı bulunamadı.");
        }
    }
}
