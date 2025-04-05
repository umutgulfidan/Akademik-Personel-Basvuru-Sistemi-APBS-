using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Dashboard
{
    public class DashboardStats
    {
        public int? OnlineUser { get; set; }

        public int? TotalUsers { get; set; }
        public int? ActiveUsers { get; set; }
        public int? PassiveUsers { get; set; }

        public int? TotalIlan { get; set; }
        public int? ActiveIlan { get; set; }
        public int? PassiveIlan { get; set; }

        public int? TotalAlan { get; set; }
        public int? TotalBolum { get; set; }

        public int? TotalKriter { get; set; }
        public int? TotalAlanKriteri { get; set; }
        public int? TotalPuanKriteri { get; set; }

        public int? TotalBildirim { get; set; }
        public int? TotalReadBildirim { get; set; }
        public int? TotalUnreadBildirim { get; set; }
    }
}
