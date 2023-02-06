using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Services
{
    public interface IDtoServiceAsync<TReadDto, in TCreateDto> : IAsyncDisposable
        where TReadDto : class
        where TCreateDto : class
    {
        Task<List<TReadDto>> GetAllAsync();

        Task<TReadDto> GetByIdAsync(int id);

        Task<TReadDto> UpdateAsync(TReadDto item);

        Task<TReadDto> CreateAsync(TCreateDto item);

        Task DeleteAsync(int id);
    }
}
