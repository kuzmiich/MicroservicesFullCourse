using AutoMapper;
using KuzmichInc.Microservices.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.Services
{
    public abstract class BaseService<TDto, TEntity> : IDtoService<TDto>
        where TDto : class
        where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        private bool _disposed;

        public BaseService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAll()
        {
            var entities = await _repository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto> GetById(int id)
        {
            var entity = await _repository.GetById(id);

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> Update(TDto item)
        {
            var mappedEntity = _mapper.Map<TEntity>(item);
            var updatedEntity = _repository.Update(mappedEntity);

            await _repository.SaveChanges();

            return _mapper.Map<TDto>(updatedEntity);
        }

        public async Task<TDto> Create(TDto item)
        {
            var entity = _mapper.Map<TEntity>(item);

            var createdEntity = await _repository.Create(entity);
            await _repository.SaveChanges();

            return _mapper.Map<TDto>(createdEntity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
            await _repository.SaveChanges();
        }

        #region Dispose Service

        #endregion
    }
}
