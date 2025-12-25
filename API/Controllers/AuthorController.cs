using Microsoft.AspNetCore.Mvc;
using NetCoreWebApi.Application.Dtos.Author;
using NetCoreWebApi.Application.Interfaces.Services;

namespace NetCoreWebApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _authorService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDto dto)
        {
            await _authorService.CreateAsync(dto);
            return Ok();
        }

    }
}
