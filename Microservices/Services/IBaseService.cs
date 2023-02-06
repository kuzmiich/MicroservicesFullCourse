namespace Microservices.Services
{
    public interface IBaseService<TReadDto, in TCreateDto> : 
        IDtoService<TReadDto, TCreateDto>, 
        IDtoServiceAsync<TReadDto, TCreateDto>
        where TReadDto : class
        where TCreateDto : class
    {
    }
}
