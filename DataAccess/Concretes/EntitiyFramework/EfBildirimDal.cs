using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Dtos.Bildirim;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Concretes.EntitiyFramework
{
    public class EfBildirimDal : EfRepositoryBase<Context, Bildirim>, IBildirimDal
    {
        public async Task<List<Bildirim>> GetAllWithPaginating(BildirimQueryDto query)
        {
            using var context = new Context();
            // Veritabanı sorgusunu başlatıyoruz
            var bildirimsQuery = context.Bildirimler.AsNoTracking().AsQueryable();


            // Filtreleme işlemleri
            if (query.Id.HasValue)
            {
                bildirimsQuery = bildirimsQuery.Where(u => u.Id == query.Id);
            }
            if (!string.IsNullOrEmpty(query.Baslik))
            {
                bildirimsQuery = bildirimsQuery.Where(u => EF.Functions.Like(u.Baslik, $"%{query.Baslik}%"));
            }
            if (query.KullaniciId.HasValue)
            {
                bildirimsQuery = bildirimsQuery.Where(u => u.KullaniciId == query.KullaniciId);
            }
            if (query.MinTarih.HasValue)
            {
                bildirimsQuery = bildirimsQuery.Where(x => x.OlusturmaTarihi >= query.MinTarih);
            }
            if (query.MaxTarih.HasValue)
            {
                bildirimsQuery = bildirimsQuery.Where(x => x.OlusturmaTarihi <= query.MaxTarih);
            }
            if (query.Status.HasValue)
            {
                bildirimsQuery = bildirimsQuery.Where(x=>x.Status == query.Status);
            }



            // Sıralama işlemleri
            if (query.SortBy.ToLower() == "baslik")
            {
                bildirimsQuery = query.IsDescending ? bildirimsQuery.OrderByDescending(u => u.Baslik) : bildirimsQuery.OrderBy(u => u.Baslik);
            }
            else if (query.SortBy.ToLower() == "tarih")
            {
                bildirimsQuery = query.IsDescending ? bildirimsQuery.OrderByDescending(u => u.OlusturmaTarihi) : bildirimsQuery.OrderBy(u => u.OlusturmaTarihi);
            }else if (query.SortBy.ToLower() == "status")
            {
                bildirimsQuery = query.IsDescending ? bildirimsQuery.OrderByDescending(u => u.Status) : bildirimsQuery.OrderBy(u => u.Status);
            }
            else
            {
                bildirimsQuery = query.IsDescending ? bildirimsQuery.OrderByDescending(u => u.Id) : bildirimsQuery.OrderBy(u => u.Id);
            }

            // Sayfalama işlemleri
            bildirimsQuery = bildirimsQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize);

            // Asenkron olarak veritabanından sorguyu çalıştırıyoruz
            return await bildirimsQuery.ToListAsync();  // Burada ToListAsync() kullanarak veritabanından sonuçları alıyoruz.
        }
    }
}
