using NetCoreWebApi.Domain.Entities;

namespace NetCoreWebApi.Application.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task AddAsync(Author author);
    }
}
