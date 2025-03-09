using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.Dtos.Bolum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntitiyFramework
{
    public class EfBolumDal : EfRepositoryBase<Context, Bolum>, IBolumDal
    {
        public async Task<List<Bolum>> GetAllBolumsWithAlanAsync(Expression<Func<Bolum, bool>> filter = null)
        {
            await using var context = new Context();
            var values = filter==null ? await context.Set<Bolum>().Include(x=>x.Alan).ToListAsync() : await context.Bolumler.Include(x => x.Alan).Where(filter).ToListAsync();
            return values;
        }

        public async Task<Bolum> GetBolumWithAlanAsync(Expression<Func<Bolum, bool>> filter)
        {
            await using var context = new Context();
            var value =  await context.Set<Bolum>().Include(x=> x.Alan).Where(filter).SingleOrDefaultAsync();
            return value;
        }
    }
}
