using Core.DataAccess.EntityFramework;
using Entities.Concretes;
using Entities.Dtos.Ilan;
using Entities.Dtos.IlanBasvuru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IIlanBasvuruDal : IEntityRepository<IlanBasvuru>
    {
        Task<List<IlanBasvuru>> GetAllByQueryAsync(IlanBasvuruQueryDto query);
    }
}
