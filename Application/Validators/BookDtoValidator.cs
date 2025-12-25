using FluentValidation;
using NetCoreWebApi.Application.Dtos.Book;

namespace NetCoreWebApi.Application.Validators
{
    public class BookDtoValidator : AbstractValidator<BookDto>
    {
        public BookDtoValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Book title is required")
            .MaximumLength(200).WithMessage("Book title must not exceed 200 characters");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("AuthorId must be greater than zero");
        }
    }
}
