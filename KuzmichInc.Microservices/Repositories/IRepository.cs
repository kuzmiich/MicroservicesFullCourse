using System;
using System.Linq;

namespace KuzmichInc.Microservices.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> GetAll();

        T GetById(int id);

        T Create(T entity);

        T Update(T entity);

        void Delete(int id);

        bool SaveChanges();
    }
}
