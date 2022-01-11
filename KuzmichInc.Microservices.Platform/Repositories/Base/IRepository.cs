using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.Repositories.Base
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        T GetById(int id);

        T Create(T entity);

        T Update(T entity);

        void Delete(T entity);

        bool SaveChanges();
    }
}
