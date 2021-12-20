using FluentValidation;

namespace WebApi.BookOperations.AddBook
{
    public class CreateBookValidator : AbstractValidator<CreateBook>
    {
        public CreateBookValidator()
        {
            RuleFor(x=> x.Model.GenreId).GreaterThan(0);
            RuleFor(x=> x.Model.Title).NotEmpty().MinimumLength(5);
        }
    }
}