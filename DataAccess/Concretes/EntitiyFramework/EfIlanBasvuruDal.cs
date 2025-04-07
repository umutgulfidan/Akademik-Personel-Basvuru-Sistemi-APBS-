using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.Dtos.IlanBasvuru;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntitiyFramework
{
    public class EfIlanBasvuruDal : EfRepositoryBase<Context, IlanBasvuru>, IIlanBasvuruDal
    {
        public async Task<List<IlanBasvuru>> GetAllByQueryAsync(IlanBasvuruQueryDto query)
        {
            using var context = new Context();
            // Veritabanı sorgusunu başlatıyoruz
            var ilanBasvuruQuery = context.IlanBasvurulari.AsNoTracking().Include(x => x.BasvuruDurumu).AsQueryable();

            // Filtreleme işlemleri
            if (query.Id.HasValue)
            {
                ilanBasvuruQuery = ilanBasvuruQuery.Where(u => u.Id == query.Id);
            }
            if (query.IlanId.HasValue)
            {
                ilanBasvuruQuery = ilanBasvuruQuery.Where(u => u.IlanId == query.IlanId);
            }
            if (query.BasvuranId.HasValue)
            {
                ilanBasvuruQuery = ilanBasvuruQuery.Where(u => u.BasvuranId == query.BasvuranId);
            }
            if (query.BasvuruDurumuId.HasValue)
            {
                ilanBasvuruQuery = ilanBasvuruQuery.Where(u => u.BasvuruDurumuId == query.BasvuruDurumuId);
            }


            // Sıralama işlemleri
            if (query.SortBy.ToLower() == "id")
            {
                ilanBasvuruQuery = query.IsDescending ? ilanBasvuruQuery.OrderByDescending(u => u.Id) : ilanBasvuruQuery.OrderBy(u => u.Id);
            }
            else if (query.SortBy.ToLower() == "basvurutarihi")
            {
                ilanBasvuruQuery = query.IsDescending ? ilanBasvuruQuery.OrderByDescending(u => u.BasvuruTarihi) : ilanBasvuruQuery.OrderBy(u => u.BasvuruTarihi);
            }
            else
            {
                ilanBasvuruQuery = query.IsDescending ? ilanBasvuruQuery.OrderByDescending(u => u.Id) : ilanBasvuruQuery.OrderBy(u => u.Id);
            }

            // Sayfalama işlemleri
            ilanBasvuruQuery = ilanBasvuruQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize);

            // Asenkron olarak veritabanından sorguyu çalıştırıyoruz
            return await ilanBasvuruQuery.ToListAsync();  // Burada ToListAsync() kullanarak veritabanından sonuçları alıyoruz.
        }
    }
}
