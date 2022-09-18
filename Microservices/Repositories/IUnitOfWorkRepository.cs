namespace Microservices.Repositories
{
    public interface IUnitOfWorkRepository<TEntity> : IRepository<TEntity>, IRepositoryAsync<TEntity>
    {
    }
}
