using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        Task<T> GetById(int id);

        Task<T> Create(T entity);

        T Update(T entity);

        Task Delete(int id);

        Task SaveChanges();
    }
}
