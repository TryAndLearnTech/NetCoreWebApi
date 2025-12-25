using NetCoreWebApi.Application.Dtos;
using NetCoreWebApi.Domain.Entities;

namespace NetCoreWebApi.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(int Id, CancellationToken cancellationToken);
        Task<(IEnumerable<T> Data, int TotalCount)> GetPagedAsync(QueryParameters queryParameters);
    }
}
