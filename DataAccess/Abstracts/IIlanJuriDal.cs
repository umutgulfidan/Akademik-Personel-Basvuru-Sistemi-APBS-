using Core.DataAccess.EntityFramework;
using DataAccess.Concretes;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IIlanJuriDal : IEntityRepository<IlanJuri>
    {
        Task<List<IlanJuri>> GetAllWithIncludes(Expression<Func<IlanJuri, bool>> filter = null);
        Task<IlanJuri> GetWithIncludes(Expression<Func<IlanJuri, bool>> filter);
    }
}
