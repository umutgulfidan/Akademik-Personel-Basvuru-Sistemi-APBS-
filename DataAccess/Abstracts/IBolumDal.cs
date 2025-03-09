using Core.DataAccess.EntityFramework;
using Core.Entities;
using Entities.Concretes;
using Entities.Dtos.Bolum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IBolumDal : IEntityRepository<Bolum>
    {
        Task<List<Bolum>> GetAllBolumsWithAlanAsync(Expression<Func<Bolum, bool>> filter = null);
        Task<Bolum> GetBolumWithAlanAsync(Expression<Func<Bolum, bool>> filter);
    }
}
