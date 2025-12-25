using Microsoft.EntityFrameworkCore;
using NetCoreWebApi.Domain.Entities;

namespace NetCoreWebApi.Application.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        // Added additional method ... .... ....

        // The following methods are already implemented
        //Task<IEnumerable<Book>> GetAllAsync();
        //Task AddAsync(Book book);
        //Task<Book> GetByIdAsync(int id, CancellationToken cancellationToken);
        //Task<Book> UpdateAsync(Book book, CancellationToken cancellationToken);
        //Task DeleteAsync(int Id, CancellationToken cancellationToken);
    }
}
