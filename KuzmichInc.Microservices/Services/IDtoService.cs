using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.Services
{
    public interface IDtoService<TResponseDto, TRequestDto> : IDisposable, IAsyncDisposable
        where TResponseDto : class
        where TRequestDto : class
    {
        Task<IEnumerable<TResponseDto>> GetAll();

        Task<TResponseDto> GetById(int id);

        Task<TResponseDto> Update(TResponseDto item);

        Task<TResponseDto> Create(TRequestDto item);

        Task Delete(int id);
    }
}