using Core.DataAccess.EntityFramework;
using Entities.Concretes;
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
        Task<List<Ilan>> GetAllWithBolumAndPozisyon(Expression<Func<Ilan, bool>> filter = null);
        Task<Ilan> GetWithBolumAndPozisyon(Expression<Func<Ilan, bool>> filter);
    }
}
