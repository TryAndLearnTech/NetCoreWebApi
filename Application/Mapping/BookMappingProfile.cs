using AutoMapper;
using NetCoreWebApi.Application.Dtos.Book;
using NetCoreWebApi.Domain.Entities;

namespace NetCoreWebApi.Application.Mapping
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDtoCommand, Book>();

            // Entity ➜ DTO
            CreateMap<Book, BookDto>()
                .ForMember(
                    dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author != null ? src.Author.Name : null)
                );
            /*
            // DTO ➜ Entity (ignore navigation properties)
            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.Author, opt => opt.Ignore());
            */
        }
    }
}
