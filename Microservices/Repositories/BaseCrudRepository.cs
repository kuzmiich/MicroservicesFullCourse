﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Helpers;

namespace Microservices.Repositories
{
    public abstract class BaseCrudRepository<TContext, TEntity> : IBaseRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        protected readonly DbSet<TEntity> Set;
        private readonly TContext _context;

        private bool _disposed;
        private readonly object _locker = new ();

        public BaseCrudRepository(TContext context)
        {
            _context = context;
            Set = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll() => Set.AsNoTracking().DefaultIfEmpty();

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            RepositoryExceptionHelper.IsIdValid(id);

            var foundEntity = await _context.FindAsync<TEntity>(id);

            RepositoryExceptionHelper.IsEntityExists(foundEntity, typeof(TEntity).FullName);

            return foundEntity;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            RepositoryExceptionHelper.IsEntityExists(entity, typeof(TEntity).FullName);

            var addedEntity = (await _context.AddAsync(entity)).Entity;

            return addedEntity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var deletedEntity = await GetByIdAsync(id);

            _context.Remove(deletedEntity);
        }

        public virtual Task SaveChangesAsync() => _context.SaveChangesAsync();

        public virtual TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public TEntity GetById(int id)
        {
            RepositoryExceptionHelper.IsIdValid(id);

            var foundEntity = _context.Find<TEntity>(id);

            RepositoryExceptionHelper.IsEntityExists(foundEntity, typeof(TEntity).FullName);

            return foundEntity;
        }

        public TEntity Create(TEntity entity)
        {
            RepositoryExceptionHelper.IsEntityExists(entity, typeof(TEntity).FullName);

            var addedEntity = _context.Add(entity).Entity;

            return addedEntity;
        }

        public void Delete(int id)
        {
            var deletedEntity = GetById(id);

            _context.Remove(deletedEntity);
        }

        public bool SaveChanges() => _context.SaveChanges() > 0;

        #region Dispose Repository

        public ValueTask DisposeAsync() => _context.DisposeAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                lock (_locker)
                {
                    if (disposing)
                        DisposeResourses();
                }
            }

            _disposed = true;
        }

        private void DisposeResourses()
        {
            _context.Dispose();
        }

        #endregion
    }
}
