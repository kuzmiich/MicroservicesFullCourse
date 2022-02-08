using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Services
{
    public interface IDtoService<TResponseDto, TRequestDto> : IDisposable
        where TResponseDto : class
        where TRequestDto : class
    {
        List<TResponseDto> GetAll();

        TResponseDto GetById(int id);

        TResponseDto Update(TResponseDto item);

        TResponseDto Create(TRequestDto item);

        void Delete(int id);
    }
}