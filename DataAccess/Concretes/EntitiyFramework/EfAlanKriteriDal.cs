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
    public class EfAlanKriteriDal : EfRepositoryBase<Context, AlanKriteri>, IAlanKriteriDal
    {
        public async Task<List<AlanKriteri>> GetAllWithIncludesAsync(Expression<Func<AlanKriteri, bool>> filter = null)
        {
            await using var context = new Context();
            var values = filter == null ? await context.Set<AlanKriteri>().AsNoTracking().Include(x => x.Kriter).Include(x => x.Alan).Include(x=> x.Pozisyon).ToListAsync() : await context.AlanKriterleri.AsNoTracking().Include(x => x.Kriter).Include(x => x.Alan).Include(x => x.Pozisyon).Where(filter).ToListAsync();
            return values;

        }

        public async Task<List<AlanKriteri>> GetAllWithKriterAsync(Expression<Func<AlanKriteri, bool>> filter = null)
        {
            await using var context = new Context();
            var values = filter == null ? await context.Set<AlanKriteri>().AsNoTracking().Include(x => x.Kriter).ToListAsync() : await context.AlanKriterleri.AsNoTracking().Include(x => x.Kriter).Where(filter).ToListAsync();
            return values;

        }

        public async Task<AlanKriteri> GetWithIncludesAsync(Expression<Func<AlanKriteri, bool>> filter)
        {
            await using var context = new Context();
            var value = await context.Set<AlanKriteri>().AsNoTracking().Include(x => x.Kriter).Include(x => x.Alan).Include(x => x.Pozisyon).Where(filter).SingleOrDefaultAsync();
            return value;

        }
    }
}
