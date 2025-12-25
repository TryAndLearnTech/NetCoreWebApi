using Microsoft.AspNetCore.Mvc;
using NetCoreWebApi.Application.Interfaces.Services;

namespace NetCoreWebApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TDto, TCreateDto, TUpdateDto, TDeleteCommand> : ControllerBase
    where TCreateDto : class
    where TDto : class
    {
        protected readonly IService<TDto, TCreateDto, TUpdateDto, TDeleteCommand> _createService;
        // protected readonly IReadService<TDto> _readService;

        protected BaseController(
            IService<TDto, TCreateDto, TUpdateDto, TDeleteCommand> createService
            // , IReadService<TDto> readService
            )
        {
            _createService = createService;
            // _readService = readService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _createService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
            => Ok(await _createService.GetByIdAsync(id, ct));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TCreateDto dto)
        {
            await _createService.CreateAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(TUpdateDto command, CancellationToken cancellationToken)
        {
            if (command == null)
                return BadRequest("Id mismatch");

            var updatedBook = await _createService.UpdateAsync(command, cancellationToken).ConfigureAwait(false);
            return Ok(updatedBook);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(TDeleteCommand command, CancellationToken cancellationToken)
        {
            await _createService.DeleteAsync(command, cancellationToken).ConfigureAwait(false);
            return Ok();
        }
    }
}
