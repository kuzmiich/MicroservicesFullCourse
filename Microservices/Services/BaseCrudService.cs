using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Repositories;

namespace Microservices.Services
{
    public abstract class BaseCrudService<TReadDto, TCreateDto, TEntity> :
        IUnitOfWorkService<TReadDto, TCreateDto>
        where TReadDto : class
        where TCreateDto : class
        where TEntity : class
    {
        protected readonly IUnitOfWorkRepository<TEntity> Repository;
        protected readonly IMapper Mapper;

        public BaseCrudService(IUnitOfWorkRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<List<TReadDto>> GetAllAsync()
        {
            var entities = await Repository.GetAll().ToListAsync();

            return Mapper.Map<List<TReadDto>>(entities);
        }

        public virtual async Task<TReadDto> GetByIdAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);

            return Mapper.Map<TReadDto>(entity);
        }

        public virtual async Task<TReadDto> UpdateAsync(TReadDto item)
        {
            var mappedEntity = Mapper.Map<TEntity>(item);
            var updatedEntity = Repository.Update(mappedEntity);

            await Repository.SaveChangesAsync();

            return Mapper.Map<TReadDto>(updatedEntity);
        }

        public virtual async Task<TReadDto> CreateAsync(TCreateDto item)
        {
            var entity = Mapper.Map<TEntity>(item);

            var createdEntity = await Repository.CreateAsync(entity);
            await Repository.SaveChangesAsync();

            return Mapper.Map<TReadDto>(createdEntity);

        }

        public virtual async Task DeleteAsync(int id)
        {
            await Repository.DeleteAsync(id);
            await Repository.SaveChangesAsync();
        }

        public List<TReadDto> GetAll()
        {
            var entities = Repository.GetAll();

            return Mapper.Map<List<TReadDto>>(entities);
        }

        public TReadDto GetById(int id)
        {
            var entity = Repository.GetById(id);

            return Mapper.Map<TReadDto>(entity);
        }

        public TReadDto Update(TReadDto item)
        {
            var mappedEntity = Mapper.Map<TEntity>(item);
            var updatedEntity = Repository.Update(mappedEntity);

            Repository.SaveChanges();

            return Mapper.Map<TReadDto>(updatedEntity);
        }

        public TReadDto Create(TCreateDto item)
        {
            var entity = Mapper.Map<TEntity>(item);

            var createdEntity = Repository.Create(entity);
            Repository.SaveChanges();

            return Mapper.Map<TReadDto>(createdEntity);
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
            Repository.SaveChanges();
        }


        #region Dispose Service

        public ValueTask DisposeAsync() => Repository.DisposeAsync();

        public void Dispose() => Repository.Dispose();

        #endregion
    }
}
