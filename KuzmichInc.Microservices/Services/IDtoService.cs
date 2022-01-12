using System.Collections.Generic;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.Services
{
    public interface IDtoService<TDto> where TDto : class
    {
        Task<IEnumerable<TDto>> GetAll();

        Task<TDto> GetById(int id);

        Task<TDto> Update(TDto item);

        Task<TDto> Create(TDto item);

        Task Delete(int id);
    }
}