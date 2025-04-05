using Business.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        // DashboardStatsService'i constructor üzerinden alıyoruz
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET api/dashboard/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            // İstatistikleri alıyoruz
            var dashboardStats = await _dashboardService.GetDashboardStatsAsync();

            // İstatistikler başarıyla alındıysa, veriyi JSON formatında döndürüyoruz
            return Ok(dashboardStats);
        }
    }
}
