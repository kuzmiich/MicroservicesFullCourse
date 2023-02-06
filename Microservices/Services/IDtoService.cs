using System;
using System.Collections.Generic;

namespace Microservices.Services
{
    public interface IDtoService<TReadDto, in TCreateDto> : IDisposable
        where TReadDto : class
        where TCreateDto : class
    {
        List<TReadDto> GetAll();

        TReadDto GetById(int id);

        TReadDto Update(TReadDto item);

        TReadDto Create(TCreateDto item);

        void Delete(int id);
    }
}