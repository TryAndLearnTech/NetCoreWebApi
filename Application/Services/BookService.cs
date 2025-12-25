using AutoMapper;
using NetCoreWebApi.Application.Dtos;
using NetCoreWebApi.Application.Dtos.Book;
using NetCoreWebApi.Application.Interfaces.Repositories;
using NetCoreWebApi.Application.Interfaces.Services;
using NetCoreWebApi.Domain.Entities;

namespace NetCoreWebApi.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, 
            IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetByIdAsync(int Id, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(Id, cancellationToken);
            return _mapper.Map<BookDto>(book);
        }

        public async Task CreateAsync(CreateBookDto dto)
        {
            var authorExists = await _authorRepository.GetByIdAsync(dto.AuthorId);
            if (authorExists is null)
            {
                throw new ArgumentException("Author not found");
            }
            var book = _mapper.Map<Book>(dto);
            await _bookRepository.AddAsync(book);
        }

        public async Task<BookDto> UpdateAsync(UpdateBookDtoCommand updateBookDtoCommand, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(updateBookDtoCommand);
            var updatedBook =  await _bookRepository.UpdateAsync(book, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<BookDto>(updatedBook);
        }

        public async Task<(IEnumerable<BookDto> Data, int TotalCount)> GetPagedAsync(QueryParameters queryParameters)
        {
            var (books, totalCount) = await _bookRepository.GetPagedAsync(queryParameters).ConfigureAwait(false);

            return (_mapper.Map<IEnumerable<BookDto>>(books), totalCount);
        }

        public async Task CreateAsync<TDto>(TDto dto) where TDto : class
        {
            var book = _mapper.Map<Book>(dto);
            await _bookRepository.AddAsync(book);
        }

        public async Task DeleteAsync(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            await _bookRepository.DeleteAsync(command.Id, cancellationToken).ConfigureAwait(false);
        }
    }
}
