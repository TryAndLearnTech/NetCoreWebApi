using AutoMapper;
using NetCoreWebApi.Application.Dtos.Author;
using NetCoreWebApi.Application.Interfaces.Repositories;
using NetCoreWebApi.Application.Interfaces.Services;
using NetCoreWebApi.Domain.Entities;

namespace NetCoreWebApi.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(AuthorDto dto)
        {
            var author = _mapper.Map<Author>(dto);
            await _repository.AddAsync(author);
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }
    }
}
