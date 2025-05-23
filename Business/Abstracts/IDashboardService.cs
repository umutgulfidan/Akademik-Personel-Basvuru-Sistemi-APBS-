﻿using Entities.Dtos.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IDashboardService
    {
        Task<DashboardStats> GetDashboardStatsAsync();
    }
}
