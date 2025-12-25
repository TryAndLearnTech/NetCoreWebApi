using AutoMapper;
using NetCoreWebApi.Application.Dtos.Author;
using NetCoreWebApi.Domain.Entities;

namespace NetCoreWebApi.Application.Mapping
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
        }
    }
}
