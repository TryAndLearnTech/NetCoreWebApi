using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApi.Application.Interfaces.Services;
using NetCoreWebApi.Application.Services;

namespace NetCoreWebApi.API.Controllers
{
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly IService<T> _service;
        private readonly IMapper _mapper;

        public BaseController(IService<T> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id, CancellationToken cancellationToken)
        {
            var entity = await _service.GetByIdAsync(id, cancellationToken);
            return Ok(entity);
        }


    }
}
