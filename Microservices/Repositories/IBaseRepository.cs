namespace Microservices.Repositories
{
    public interface IBaseRepository<TEntity> : IRepository<TEntity>, IRepositoryAsync<TEntity>
    {
    }
}
