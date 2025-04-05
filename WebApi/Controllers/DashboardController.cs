using Business.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IHubContext<NotificationHub> _hubContext;

        // DashboardStatsService'i constructor üzerinden alıyoruz
        public DashboardController(IDashboardService dashboardService, IHubContext<NotificationHub> hubContext)
        {
            _dashboardService = dashboardService;
            _hubContext = hubContext;
        }

        // GET api/dashboard/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            // İstatistikleri alıyoruz
            var dashboardStats = await _dashboardService.GetDashboardStatsAsync();
            dashboardStats.OnlineUser = NotificationHub.GetAuthenticatedUserCount(); // NotificationHub'dan online kullanıcı sayısını alıyoruz.
            dashboardStats.VisitorCount = NotificationHub.GetUnauthenticatedUserCount();

            // İstatistikler başarıyla alındıysa, veriyi JSON formatında döndürüyoruz
            return Ok(dashboardStats);
        }
    }
}
