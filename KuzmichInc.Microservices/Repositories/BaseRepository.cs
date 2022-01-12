using KuzmichInc.Microservices.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.Repositories
{
    public class BaseRepository<TContext, TEntity> : IRepository<TEntity>, IDisposable, IAsyncDisposable
        where TContext : DbContext
        where TEntity : class
    {
        private bool _disposed;
        private readonly TContext _context;
        protected readonly DbSet<TEntity> Set;

        public BaseRepository(TContext context)
        {
            _context = context;
            Set = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return Set.AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            RepositoryExceptionHelper.IsIdValid(id);

            var foundEntity = await Set.FindAsync(id);

            RepositoryExceptionHelper.IsEntityExists(foundEntity, typeof(TEntity).FullName);

            return foundEntity;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            RepositoryExceptionHelper.IsEntityExists(entity, typeof(TEntity).FullName);

            var addedEntity = (await _context.AddAsync(entity)).Entity;

            return addedEntity;
        }
        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task Delete(int id)
        {
            var deletedEntity = await GetById(id);

            RepositoryExceptionHelper.IsEntityExists(deletedEntity, typeof(TEntity).FullName);

            _context.Remove(deletedEntity);
        }


        #region Dispose Repository

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        #endregion
    }
}
