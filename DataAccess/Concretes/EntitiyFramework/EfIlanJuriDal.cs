using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntitiyFramework
{
    public class EfIlanJuriDal : EfRepositoryBase<Context, IlanJuri>, IIlanJuriDal
    {
        public async Task<List<IlanJuri>> GetAllWithIncludes(Expression<Func<IlanJuri, bool>> filter = null)
        {
            await using var context = new Context();
            var values = filter == null ? await context.Set<IlanJuri>().AsNoTracking().Include(x => x.Kullanici).ToListAsync() : await context.IlanJurileri.AsNoTracking().Include(x => x.Kullanici).Where(filter).ToListAsync();
            return values;
        }

        public async Task<IlanJuri> GetWithIncludes(Expression<Func<IlanJuri, bool>> filter)
        {
            await using var context = new Context();
            var value = await context.Set<IlanJuri>().AsNoTracking().Include(x=>x.Kullanici).Where(filter).SingleOrDefaultAsync();
            return value;
        }
    }
}
