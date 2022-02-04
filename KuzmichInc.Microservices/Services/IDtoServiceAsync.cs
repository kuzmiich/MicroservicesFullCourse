using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.Services
{
    public interface IDtoServiceAsync<TResponseDto, TRequestDto> : IAsyncDisposable
        where TResponseDto : class
        where TRequestDto : class
    {
        Task<List<TResponseDto>> GetAllAsync();

        Task<TResponseDto> GetByIdAsync(int id);

        Task<TResponseDto> UpdateAsync(TResponseDto item);

        Task<TResponseDto> CreateAsync(TRequestDto item);

        Task DeleteAsync(int id);
    }
}
