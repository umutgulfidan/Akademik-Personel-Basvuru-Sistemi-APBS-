using Core.DataAccess.EntityFramework;
using Entities.Dtos.Bildirim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IBildirimDal : IEntityRepository<Bildirim>
    {
        Task<List<Bildirim>> GetAllWithPaginating(AdminBildirimQueryDto bildirimQueryDto);
        Task<List<Bildirim>> GetMyPaginatedNotifications(int userId,UserBildirimQueryDto userBildirimQueryDto);
    }
}
