using AutoMapper;
using KuzmichInc.Microservices.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.Services
{
    public abstract class BaseService<TResponseDto, TRequest, TEntity> : IDtoService<TResponseDto, TRequest>
        where TResponseDto : class
        where TRequest : class
        where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TResponseDto>> GetAll()
        {
            var entities = await _repository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<TResponseDto>>(entities);
        }

        public virtual async Task<TResponseDto> GetById(int id)
        {
            var entity = await _repository.GetById(id);

            return _mapper.Map<TResponseDto>(entity);
        }

        public virtual async Task<TResponseDto> Update(TResponseDto item)
        {
            var mappedEntity = _mapper.Map<TEntity>(item);
            var updatedEntity = _repository.Update(mappedEntity);

            await _repository.SaveChanges();

            return _mapper.Map<TResponseDto>(updatedEntity);
        }

        public virtual async Task<TResponseDto> Create(TRequest item)
        {
            var entity = _mapper.Map<TEntity>(item);

            var createdEntity = await _repository.Create(entity);
            await _repository.SaveChanges();

            return _mapper.Map<TResponseDto>(createdEntity);
        }

        public virtual async Task Delete(int id)
        {
            await _repository.Delete(id);
            await _repository.SaveChanges();
        }

        #region Dispose Service

        public void Dispose() => _repository.Dispose();

        public ValueTask DisposeAsync() => _repository.DisposeAsync();

        #endregion
    }
}
