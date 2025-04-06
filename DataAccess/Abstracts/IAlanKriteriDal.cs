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
    public interface IAlanKriteriDal : IEntityRepository<AlanKriteri>
    {
        Task<List<AlanKriteri>> GetAllWithIncludesAsync(Expression<Func<AlanKriteri, bool>> filter = null);
        Task<List<AlanKriteri>> GetAllWithKriterAsync(Expression<Func<AlanKriteri, bool>> filter = null);
        Task<AlanKriteri> GetWithIncludesAsync(Expression<Func<AlanKriteri, bool>> filter);
    }
}
