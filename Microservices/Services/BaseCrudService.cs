﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Repositories;

namespace Microservices.Services
{
    public abstract class BaseCrudService<TReadDto, TCreateDto, TEntity> :
        IBaseService<TReadDto, TCreateDto>
        where TReadDto : class
        where TCreateDto : class
        where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> Repository;
        protected readonly IMapper Mapper;

        public BaseCrudService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        #region Async Queries & Commands

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

        #endregion

        #region Async Queries & Commands

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

        #endregion
        
        #region Dispose Service

        public ValueTask DisposeAsync() => Repository.DisposeAsync();

        public void Dispose() => Repository.Dispose();

        #endregion
    }
}
