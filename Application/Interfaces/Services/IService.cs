using NetCoreWebApi.Application.Dtos;

namespace NetCoreWebApi.Application.Interfaces.Services
{
    public interface IService <TDto, TCreateCommand, TUpdateCommand, TDeleteCommand>
    {
        Task CreateAsync(TCreateCommand command);
        Task<TDto> UpdateAsync(TUpdateCommand command, CancellationToken cancellationToken);
        Task DeleteAsync(TDeleteCommand command, CancellationToken cancellationToken);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id, CancellationToken ct);
        Task<(IEnumerable<TDto> Data, int TotalCount)> GetPagedAsync(QueryParameters queryParameters);
    }
}
