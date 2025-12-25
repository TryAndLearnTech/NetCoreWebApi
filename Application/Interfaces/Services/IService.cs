using NetCoreWebApi.Application.Dtos;
using NetCoreWebApi.Application.Dtos.Book;

namespace NetCoreWebApi.Application.Interfaces.Services
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(CreateBookDto dto);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<T> UpdateAsync(UpdateBookDtoCommand book, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<(IEnumerable<T> Data, int TotalCount)> GetPagedAsync(QueryParameters queryParameters);
    }
}
