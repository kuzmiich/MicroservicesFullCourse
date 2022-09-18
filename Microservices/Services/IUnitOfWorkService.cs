namespace Microservices.Services
{
    public interface IUnitOfWorkService<TReadDto, TCreateDto> : 
        IDtoService<TReadDto, TCreateDto>, 
        IDtoServiceAsync<TReadDto, TCreateDto>
        where TReadDto : class
        where TCreateDto : class
    {
    }
}
