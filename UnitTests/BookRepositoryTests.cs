using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApi.Application.Dtos;
using NetCoreWebApi.Domain.Entities;
using NetCoreWebApi.Infrastructure.Data;
using NetCoreWebApi.Infrastructure.Repositories;
using NUnit.Framework;

namespace NetCoreWebApi.UnitTests
{
    [TestFixture]
    public class BookRepositoryTests
    {
        private AppDbContext _context = null!;
        private BookRepository _repository = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            _context = new AppDbContext(options);
            _repository = new BookRepository(_context);
            SeedData();
        }

        private void SeedData()
        {
            var author = new Author { Name = "Test Author" };

            var books = new List<Book>
        {
            new() { Title = "C# Basics", Author = author },
            new() { Title = "Advanced C#", Author = author },
            new() { Title = "ASP.NET Core", Author = author }
        };

            _context.Authors.Add(author);
            _context.Books.AddRange(books);
            _context.SaveChanges();
        }

        [Test]
        public async Task AddAsync_Should_Add_Book()
        {
            var book = new Book
            {
                Title = "New Book",
                AuthorId = _context.Authors.First().Id
            };

            await _repository.AddAsync(book);

            _context.Books.Count().Should().Be(4);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_All_Books_With_Authors()
        {
            var result = await _repository.GetAllAsync();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(3);

        }

        [Test]
        public async Task GetByIdAsync_Should_Return_Book_When_Found()
        {
            var bookId = _context.Books.First().Id;

            var result = await _repository.GetByIdAsync(bookId, CancellationToken.None);

            result.Should().NotBeNull();
            result!.Title.Should().Be("C# Basics");
            result.Author.Should().NotBeNull();
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
        {
            var result = await _repository.GetByIdAsync(999, CancellationToken.None);

            result.Should().BeNull();
        }
        // -------------------- UpdateAsync --------------------

        [Test]
        public async Task UpdateAsync_Should_Update_Book()
        {
            var book = _context.Books.First();
            book.Title = "Updated Title";

            var updatedBook = await _repository.UpdateAsync(book, CancellationToken.None);

            updatedBook.Should().NotBeNull();
            updatedBook!.Title.Should().Be("Updated Title");
        }

        // -------------------- DeleteAsync --------------------

        [Test]
        public async Task DeleteAsync_Should_Remove_Book()
        {
            var bookId = _context.Books.First().Id;

            await _repository.DeleteAsync(bookId, CancellationToken.None);

            _context.Books.Count().Should().Be(2);
        }

        // -------------------- GetPagedAsync --------------------

        [Test]
        public async Task GetPagedAsync_Should_Return_Paged_Data()
        {
            var queryParams = new QueryParameters
            {
                PageNumber = 1,
                PageSize = 2
            };

            var (data, totalCount) = await _repository.GetPagedAsync(queryParams);

            totalCount.Should().Be(3);
            data.Should().HaveCount(2);
        }

        [Test]
        public async Task GetPagedAsync_Should_Filter_By_Title()
        {
            var queryParams = new QueryParameters
            {
                Filter = "C#",
                PageNumber = 1,
                PageSize = 10
            };

            var (data, totalCount) = await _repository.GetPagedAsync(queryParams);

            totalCount.Should().Be(2);
            data.All(b => b.Title.Contains("C#")).Should().BeTrue();
        }

        [Test]
        public async Task GetPagedAsync_Should_Sort_By_Title_Descending()
        {
            var queryParams = new QueryParameters
            {
                SortBy = "title",
                SortDescending = true,
                PageNumber = 1,
                PageSize = 10
            };

            var (data, _) = await _repository.GetPagedAsync(queryParams);

            data.First().Title.Should().Be("C# Basics");
        }

    }
}
