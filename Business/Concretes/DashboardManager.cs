using Business.Abstracts;
using DataAccess.Concretes;
using Entities.Dtos.Dashboard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class DashboardManager : IDashboardService
    {
        private readonly Context _context;

        public DashboardManager(Context context)
        {
            _context = context;
        }

        public async Task<DashboardStats> GetDashboardStatsAsync()
        {

            var totalUsers = await _context.Users.CountAsync();
            var activeUsers = await _context.Users.CountAsync(u => u.Status);
            var passiveUsers = await _context.Users.CountAsync(u => !u.Status);
            var totalIlan = await _context.Ilanlar.CountAsync();
            var activeIlan = await _context.Ilanlar.CountAsync(i => i.Status);
            var passiveIlan = await _context.Ilanlar.CountAsync(i => !i.Status);
            var totalAlan = await _context.Alanlar.CountAsync();
            var totalBolum = await _context.Bolumler.CountAsync();

            var totalKriter = await _context.Kriterler.CountAsync();
            var totalPuanKriteri = await _context.PuanKriterleri.CountAsync();
            var totalAlanKriteri = await _context.AlanKriterleri.CountAsync();

            var totalBildirim = await _context.Bildirimler.CountAsync();
            var totalReadBildirim = await _context.Bildirimler.CountAsync(x => x.Status);
            var totalUnreadBildirim = await _context.Bildirimler.CountAsync(x => x.Status);





            return new DashboardStats
            {
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                PassiveUsers = passiveUsers,
                TotalIlan = totalIlan,
                ActiveIlan = activeIlan,
                PassiveIlan = passiveIlan,
                TotalAlan = totalAlan,
                TotalBolum = totalBolum,
                TotalKriter = totalKriter,
                TotalAlanKriteri = totalAlanKriteri,
                TotalPuanKriteri = totalPuanKriteri,
                TotalBildirim = totalBildirim,
                TotalReadBildirim = totalReadBildirim,
                TotalUnreadBildirim = totalUnreadBildirim
            };
        }
    }
}
