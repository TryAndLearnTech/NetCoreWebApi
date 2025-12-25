using Microsoft.EntityFrameworkCore;
using NetCoreWebApi.Application.Interfaces.Repositories;
using NetCoreWebApi.Domain.Entities;
using NetCoreWebApi.Infrastructure.Data;

namespace NetCoreWebApi.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
            => await _context.Authors.ToListAsync();

        public async Task<Author?> GetByIdAsync(int id)
            => await _context.Authors.FindAsync(id);

        public async Task AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }
    }
}
