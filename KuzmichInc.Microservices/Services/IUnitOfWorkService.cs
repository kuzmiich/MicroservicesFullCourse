using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.Services
{
    public interface IUnitOfWorkService<TResponseDto, TRequestDto> : 
        IDtoService<TResponseDto, TRequestDto>, 
        IDtoServiceAsync<TResponseDto, TRequestDto>
        where TResponseDto : class
        where TRequestDto : class
    {
    }
}
