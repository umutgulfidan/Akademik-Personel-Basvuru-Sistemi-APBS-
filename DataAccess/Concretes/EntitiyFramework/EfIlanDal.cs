using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.Dtos.Ilan;
using Entities.Enums;
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

        public async Task<List<Ilan>> GetIlansByQueryAsync(UserIlanQueryDto query)
        {
            using var context = new Context();
            // Veritabanı sorgusunu başlatıyoruz
            var ilansQuery = context.Ilanlar.AsNoTracking().Include(x => x.Bolum).ThenInclude(y => y.Alan).Include(x => x.Pozisyon).AsQueryable();


            if (query.IlanTipi == IlanTuru.ActiveIlans)
            {
                ilansQuery = ilansQuery.Where(x => x.Status == true && x.BitisTarihi > DateTime.Now);
            }
            else if (query.IlanTipi == IlanTuru.ExpiredIlans)
            {
                ilansQuery = ilansQuery.Where(x => x.Status == true && x.BitisTarihi < DateTime.Now);
            }

            // Filtreleme işlemleri
            if (query.Id.HasValue)
            {
                ilansQuery = ilansQuery.Where(u => u.Id == query.Id);
            }
            if (!string.IsNullOrEmpty(query.Baslik))
            {
                ilansQuery = ilansQuery.Where(u => EF.Functions.Like(u.Baslik, $"%{query.Baslik}%"));
            }

            if (query.PozisyonId.HasValue)
            {
                ilansQuery = ilansQuery.Where(u => u.PozisyonId == query.PozisyonId);
            }

            if (query.BolumId.HasValue)
            {
                ilansQuery = ilansQuery.Where(u => u.BolumId == query.BolumId);
            }


            // Sıralama işlemleri
            if (query.SortBy.ToLower() == "baslik")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.Baslik) : ilansQuery.OrderBy(u => u.Baslik);
            }
            else if (query.SortBy.ToLower() == "bolumadi")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.Bolum.Ad) : ilansQuery.OrderBy(u => u.Bolum.Ad);
            }
            else if (query.SortBy.ToLower() == "pozisyonadi")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.Pozisyon.Ad) : ilansQuery.OrderBy(u => u.Pozisyon.Ad);
            }
            else if (query.SortBy.ToLower() == "baslangictarihi")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.BaslangicTarihi) : ilansQuery.OrderBy(u => u.BaslangicTarihi);
            }
            else if (query.SortBy.ToLower() == "bitistarihi")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.BitisTarihi) : ilansQuery.OrderBy(u => u.BitisTarihi);
            }
            else
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.Id) : ilansQuery.OrderBy(u => u.Id);
            }

            // Sayfalama işlemleri
            ilansQuery = ilansQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize);

            // Asenkron olarak veritabanından sorguyu çalıştırıyoruz
            return await ilansQuery.ToListAsync();  // Burada ToListAsync() kullanarak veritabanından sonuçları alıyoruz.
        }

        public async Task<List<Ilan>> GetIlansByQueryAsync(AdminIlanQueryDto query)
        {
            using var context = new Context();
            // Veritabanı sorgusunu başlatıyoruz
            var ilansQuery = context.Ilanlar.AsNoTracking().Include(x => x.Bolum).ThenInclude(y => y.Alan).Include(x => x.Pozisyon).Include(x => x.Olusturan).AsQueryable();


            if (query.IlanTipi == IlanTuru.ActiveIlans)
            {
                ilansQuery = ilansQuery.Where(x=> x.BitisTarihi > DateTime.Now);
            }
            else if (query.IlanTipi == IlanTuru.ExpiredIlans)
            {
                ilansQuery = ilansQuery.Where(x => x.BitisTarihi < DateTime.Now);
            }

            // Filtreleme işlemleri
            if (query.Id.HasValue)
            {
                ilansQuery = ilansQuery.Where(u => u.Id == query.Id);
            }
            if (!string.IsNullOrEmpty(query.Baslik))
            {
                ilansQuery = ilansQuery.Where(u => EF.Functions.Like(u.Baslik, $"%{query.Baslik}%"));
            }

            if (query.PozisyonId.HasValue)
            {
                ilansQuery = ilansQuery.Where(u => u.PozisyonId == query.PozisyonId);
            }

            if (query.BolumId.HasValue)
            {
                ilansQuery = ilansQuery.Where(u => u.BolumId == query.BolumId);
            }
            if (query.Status.HasValue)
            {
                ilansQuery = ilansQuery.Where(x=> x.Status == query.Status);
            }


            // Sıralama işlemleri
            if (query.SortBy.ToLower() == "baslik")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.Baslik) : ilansQuery.OrderBy(u => u.Baslik);
            }
            else if (query.SortBy.ToLower() == "bolumadi")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.Bolum.Ad) : ilansQuery.OrderBy(u => u.Bolum.Ad);
            }
            else if (query.SortBy.ToLower() == "pozisyonadi")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.Pozisyon.Ad) : ilansQuery.OrderBy(u => u.Pozisyon.Ad);
            }
            else if (query.SortBy.ToLower() == "baslangictarihi")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.BaslangicTarihi) : ilansQuery.OrderBy(u => u.BaslangicTarihi);
            }
            else if (query.SortBy.ToLower() == "bitistarihi")
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.BitisTarihi) : ilansQuery.OrderBy(u => u.BitisTarihi);
            }
            else
            {
                ilansQuery = query.IsDescending ? ilansQuery.OrderByDescending(u => u.Id) : ilansQuery.OrderBy(u => u.Id);
            }

            // Sayfalama işlemleri
            ilansQuery = ilansQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize);

            // Asenkron olarak veritabanından sorguyu çalıştırıyoruz
            return await ilansQuery.ToListAsync();  // Burada ToListAsync() kullanarak veritabanından sonuçları alıyoruz.
        }


        public async Task<Ilan> GetWithBolumAndPozisyon(Expression<Func<Ilan, bool>> filter)
        {
            await using var context = new Context();
            var value = await context.Set<Ilan>().Include(x => x.Pozisyon).Include(x => x.Bolum).ThenInclude(y => y.Alan).Where(filter).SingleOrDefaultAsync();
            return value;

        }
    }
}
