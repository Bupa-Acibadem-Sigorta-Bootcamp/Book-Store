using FluentValidation;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;

namespace WebApi.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    { 
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}