using Core.DataAccess.EntityFramework;
using DataAccess.Concretes;
using DataAccess.Concretes.EntitiyFramework;
using Entities.Concretes;
using Entities.Dtos.Ilan;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IIlanDal : IEntityRepository<Ilan>
    {
        Task<List<Ilan>> GetAllWithBolumPozisyonOlusturan(Expression<Func<Ilan, bool>> filter = null);
        Task<Ilan> GetWithBolumPozisyon(Expression<Func<Ilan, bool>> filter);
        Task<List<Ilan>> GetIlansByQueryAsync(UserIlanQueryDto query);
        Task<List<Ilan>> GetIlansByQueryAsync(AdminIlanQueryDto query);
        Task<List<Ilan>> GetAllWithBolumPozisyon(Expression<Func<Ilan, bool>> filter = null);



    }
}
