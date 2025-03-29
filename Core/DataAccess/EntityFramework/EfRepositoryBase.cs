using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TContext, TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        // Senkron Metotlar
        public void Add(TEntity entity)
        {
            using var context = new TContext();
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using var context = new TContext();
            context.Remove(entity);
            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            return context.Set<TEntity>().FirstOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();
            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }

        public void Update(TEntity entity)
        {
            using var context = new TContext();
            context.Update(entity);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            using var context = new TContext();
            var entity = context.Set<TEntity>().Find(id);
            if (entity == null) return;
            context.Remove(entity);
            context.SaveChanges();
        }

        // Asenkron Metotlar
        public async Task AddAsync(TEntity entity)
        {
            await using var context = new TContext();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await using var context = new TContext();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new TContext();
            return await context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await using var context = new TContext();
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await using var context = new TContext();
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null) return;
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllReadOnlyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return filter == null
                ? await context.Set<TEntity>().AsNoTracking().ToListAsync()
                : await context.Set<TEntity>().Where(filter).AsNoTracking().ToListAsync();
        }
        public async Task<TEntity> GetReadOnlyAsync(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new TContext();
            return await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
        }

    }
}
