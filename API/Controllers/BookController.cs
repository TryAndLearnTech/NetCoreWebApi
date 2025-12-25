using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApi.Application.Dtos;
using NetCoreWebApi.Application.Dtos.Book;
using NetCoreWebApi.Application.Interfaces.Services;

namespace NetCoreWebApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById (int id, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(id, cancellationToken);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            await _bookService.CreateAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBookDtoCommand command, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);

            if (book == null) 
                return BadRequest("Id mismatch");

            var updatedBook = await _bookService.UpdateAsync(command, cancellationToken).ConfigureAwait(false);
            return Ok(updatedBook);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken) 
        {
            var book = await _bookService.GetByIdAsync(Id, cancellationToken);
            if (book == null) return BadRequest("Invalid Id");
            await _bookService.DeleteAsync(book.Id, cancellationToken).ConfigureAwait(false);
            return Ok();
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetBooks([FromQuery] QueryParameters queryParameters)
        {
            var (books, totalCount) = await _bookService.GetPagedAsync(queryParameters);
                
            var response = new
            {
                Data = books,
                TotalCount = totalCount,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize
            };

            return Ok(response);
        }


    }
}
