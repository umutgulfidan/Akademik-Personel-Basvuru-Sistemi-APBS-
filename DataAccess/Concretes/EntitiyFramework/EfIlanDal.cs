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
    public class EfIlanDal : EfRepositoryBase<Context, Ilan>, IIlanDal
    {
        public async Task<List<Ilan>> GetAllWithBolumAndPozisyon(Expression<Func<Ilan, bool>> filter = null)
        {
            await using var context = new Context();
            var values = filter == null ? await context.Set<Ilan>().Include(x=>x.Pozisyon).Include(x => x.Olusturan).Include(x=>x.Bolum).ThenInclude(y=> y.Alan).ToListAsync() : await context.Ilanlar.Include(x => x.Pozisyon).Include(x => x.Olusturan).Include(x => x.Bolum).Where(filter).ToListAsync();
            return values;

        }
        public async Task<Ilan> GetWithBolumAndPozisyon(Expression<Func<Ilan, bool>> filter)
        {
            await using var context = new Context();
            var value = await context.Set<Ilan>().Include(x => x.Pozisyon).Include(x => x.Olusturan).Include(x => x.Bolum).ThenInclude(y => y.Alan).Where(filter).SingleOrDefaultAsync();
            return value;

        }
    }
}
