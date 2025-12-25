using FluentValidation;
using NetCoreWebApi.Application.Dtos.Author;

namespace NetCoreWebApi.Application.Validators
{
    public class AuthorDtoValidator : AbstractValidator<AuthorDto>
    {
        public AuthorDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Author name is required")
                .MaximumLength(100).WithMessage("Author name must not exceed 100 characters");
        }
    }
}
