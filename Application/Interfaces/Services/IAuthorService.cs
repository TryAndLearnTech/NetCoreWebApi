using NetCoreWebApi.Application.Dtos.Author;

namespace NetCoreWebApi.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task CreateAsync(AuthorDto dto);
    }
}
