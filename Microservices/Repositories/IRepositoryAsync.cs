using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Repositories
{
    public interface IRepositoryAsync<T> : IAsyncDisposable
    {
        Task<T> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
