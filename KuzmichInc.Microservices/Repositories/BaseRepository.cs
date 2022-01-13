using KuzmichInc.Microservices.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.Repositories
{
    public class BaseRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        protected readonly DbSet<TEntity> Set;
        private readonly TContext _context;

        private bool _disposed;
        private readonly object _locker = new ();

        public BaseRepository(TContext context)
        {
            _context = context;
            Set = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Set.AsNoTracking();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            RepositoryExceptionHelper.IsIdValid(id);

            var foundEntity = await Set.FindAsync(id);

            RepositoryExceptionHelper.IsEntityExists(foundEntity, typeof(TEntity).FullName);

            return foundEntity;
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            RepositoryExceptionHelper.IsEntityExists(entity, typeof(TEntity).FullName);

            var addedEntity = (await _context.AddAsync(entity)).Entity;

            return addedEntity;
        }
        public virtual Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual async Task Delete(int id)
        {
            var deletedEntity = await GetById(id);

            RepositoryExceptionHelper.IsEntityExists(deletedEntity, typeof(TEntity).FullName);

            _context.Remove(deletedEntity);
        }


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
