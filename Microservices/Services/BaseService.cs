using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Repositories;

namespace Microservices.Services
{
    public abstract class BaseService<TResponseDto, TRequestDto, TEntity> :
        IUnitOfWorkService<TResponseDto, TRequestDto>
        where TResponseDto : class
        where TRequestDto : class
        where TEntity : class
    {
        protected readonly IUnitOfWorkRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IUnitOfWorkRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<List<TResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();

            return _mapper.Map<List<TResponseDto>>(entities);
        }

        public virtual async Task<TResponseDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return _mapper.Map<TResponseDto>(entity);
        }

        public virtual async Task<TResponseDto> UpdateAsync(TResponseDto item)
        {
            var mappedEntity = _mapper.Map<TEntity>(item);
            var updatedEntity = _repository.Update(mappedEntity);

            await _repository.SaveChangesAsync();

            return _mapper.Map<TResponseDto>(updatedEntity);
        }

        public virtual async Task<TResponseDto> CreateAsync(TRequestDto item)
        {
            var entity = _mapper.Map<TEntity>(item);

            var createdEntity = await _repository.CreateAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TResponseDto>(createdEntity);

        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }

        public List<TResponseDto> GetAll()
        {
            var entities = _repository.GetAll();

            return _mapper.Map<List<TResponseDto>>(entities);
        }

        public TResponseDto GetById(int id)
        {
            var entity = _repository.GetById(id);

            return _mapper.Map<TResponseDto>(entity);
        }

        public TResponseDto Update(TResponseDto item)
        {
            var mappedEntity = _mapper.Map<TEntity>(item);
            var updatedEntity = _repository.Update(mappedEntity);

            _repository.SaveChanges();

            return _mapper.Map<TResponseDto>(updatedEntity);
        }

        public TResponseDto Create(TRequestDto item)
        {
            var entity = _mapper.Map<TEntity>(item);

            var createdEntity = _repository.Create(entity);
            _repository.SaveChanges();

            return _mapper.Map<TResponseDto>(createdEntity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.SaveChanges();
        }


        #region Dispose Service

        public ValueTask DisposeAsync() => _repository.DisposeAsync();

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
