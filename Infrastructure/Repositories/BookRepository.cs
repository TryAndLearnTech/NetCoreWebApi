using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApi.Application.Dtos;
using NetCoreWebApi.Application.Interfaces.Repositories;
using NetCoreWebApi.Domain.Entities;
using NetCoreWebApi.Infrastructure.Data;

namespace NetCoreWebApi.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book entity)
        {
            _context.Books.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        
        public async Task<Book?> UpdateAsync(Book book, CancellationToken cancellationToken)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == book.Id);    
        }

        public async Task DeleteAsync(int Id, CancellationToken cancellationToken)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == Id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Book> Data, int TotalCount)> GetPagedAsync(QueryParameters queryParameters)
        {
            IQueryable<Book> query = _context.Books.Include(b => b.Author).AsNoTracking();

            // Filtering (simple example by Title)
            if (!string.IsNullOrWhiteSpace(queryParameters.Filter))
            {
                query = query.Where(b => b.Title.Contains(queryParameters.Filter));
            }

            // Sorting (simple example)
            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy))
            {
                query = queryParameters.SortBy.ToLower() switch
                {
                    "title" => queryParameters.SortDescending ? query.OrderByDescending(b => b.Title) : query.OrderBy(b => b.Title),
                    "author" => queryParameters.SortDescending ? query.OrderByDescending(b => b.Author.Name) : query.OrderBy(b => b.Author.Name),
                    _ => query
                };
            }

            int totalCount = await query.CountAsync();

            // Pagination
            var data = await query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return (data, totalCount);

        }
    }
}
