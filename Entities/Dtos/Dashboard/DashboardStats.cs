using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Dashboard
{
    public class DashboardStats
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int PassiveUsers { get; set; }

        public int TotalIlan { get; set; }
        public int ActiveIlan { get; set; }
        public int PassiveIlan { get; set; }

        public int TotalAlan { get; set; }
        public int TotalBolum { get; set; }
    }
}
