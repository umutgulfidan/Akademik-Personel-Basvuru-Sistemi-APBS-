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
    public class EfPuanKriteriDal : EfRepositoryBase<Context, PuanKriteri>, IPuanKriteriDal
    {
        public async Task<List<PuanKriteri>> GetAllWithIncludesAsync(Expression<Func<PuanKriteri, bool>> filter = null)
        {
            await using var context = new Context();
            var values = filter == null ? await context.Set<PuanKriteri>().AsNoTracking().Include(x => x.Kriter).Include(x => x.Alan).Include(x => x.Pozisyon).ToListAsync() : await context.PuanKriterleri.AsNoTracking().Include(x => x.Kriter).Include(x => x.Alan).Include(x => x.Pozisyon).Where(filter).ToListAsync();
            return values;

        }

        public async Task<PuanKriteri> GetWithIncludesAsync(Expression<Func<PuanKriteri, bool>> filter)
        {
            await using var context = new Context();
            var value = await context.Set<PuanKriteri>().AsNoTracking().Include(x => x.Kriter).Include(x => x.Alan).Include(x => x.Pozisyon).Where(filter).SingleOrDefaultAsync();
            return value;

        }
    }
}
