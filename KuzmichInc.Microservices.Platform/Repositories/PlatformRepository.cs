using KuzmichInc.Microservices.PlatformService.Data;
using KuzmichInc.Microservices.PlatformService.Helpers;
using KuzmichInc.Microservices.PlatformService.Models;
using KuzmichInc.Microservices.PlatformService.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.Repositories
{
    public class PlatformRepository : IRepository<Platform>, IDisposable, IAsyncDisposable
    {
        private readonly PlatformContext _context;
        private bool _disposed;

        public PlatformRepository(PlatformContext context)
        {
            _context = context;
        }

        public PlatformRepository()
        {
            
        }
        public IQueryable<Platform> GetAll()
        {
            return _context.Platforms.AsNoTracking();
        }

        public Platform GetById(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public Platform Create(Platform entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Add(entity);
            return entity;
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public Platform Update(Platform entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete(Platform entity)
        {
            var deletedEntity = _context.Platforms.Find(entity);
            RepositoryExceptionHelper.IsEntityExists(deletedEntity, typeof(Platform).FullName);

            _context.Remove(entity);
            
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
