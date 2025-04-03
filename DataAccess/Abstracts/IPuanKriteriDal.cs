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
    public interface IPuanKriteriDal : IEntityRepository<PuanKriteri>
    {
        Task<List<PuanKriteri>> GetAllWithIncludesAsync(Expression<Func<PuanKriteri, bool>> filter = null);
        Task<PuanKriteri> GetWithIncludesAsync(Expression<Func<PuanKriteri, bool>> filter);
    }
}
