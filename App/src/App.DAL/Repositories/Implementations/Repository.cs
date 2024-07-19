using App.Core.Entities.Commons;
using App.DAL.Presistence;
using App.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : BaseEntity, IAuditedEntity
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<TEntity> DbSet;
        private bool _disposed = false;
        protected Repository(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return AddIncludes(DbSet.Where(predicate), includes);
        }

        public async Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[]? includes)
        {
            return await AddIncludes(DbSet, includes).FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;

            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> RecoverAsync(TEntity entity)
        {
            entity.IsDeleted = false;

            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public IQueryable<TEntity> AddIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            return includes?.Aggregate(query, (current, include) => current.Include(include)) ?? query;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
