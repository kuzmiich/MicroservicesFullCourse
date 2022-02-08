using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Repositories
{
    public interface IUnitOfWorkRepository<TEntity> : IRepository<TEntity>, IRepositoryAsync<TEntity>
    {
    }
}
