using NetCoreWebApi.Domain.Entities;
using AutoMapper;
using NetCoreWebApi.Application.Dtos.Author;
using NetCoreWebApi.Application.Dtos.Book;

namespace NetCoreWebApi.Application.Mapping
{
    //Not Used common mapper
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
