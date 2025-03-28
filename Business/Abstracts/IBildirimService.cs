using Core.Utilities.Results;
using Entities.Dtos.Bildirim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBildirimService
    {
        Task<IResult> AddAdmin(AddBildirimDto bildirim);
        Task<IResult> DeleteAdmin(int id);
        Task<IResult> UpdateAdmin(UpdateBildirimDto bildirim);
        Task<IDataResult<List<Bildirim>>> GetAllForAdmin();
        Task<IDataResult<List<Bildirim>>> GetAllByUser(int userId);
        Task<IResult> MarkAsRead(int id,int userId);
        Task<IResult> MarkAsReadAll(int userId);
        Task<IResult> MarkAsUnread(int id, int userId);
        Task<IResult> DeleteByUser(int id, int userId);
        Task<IResult> DeleteAllByUser(int userId);

    }
}
