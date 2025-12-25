using Microsoft.EntityFrameworkCore;
using NetCoreWebApi.Domain.Entities;

namespace NetCoreWebApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
