using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(x => x.Model.SurName).NotEmpty().Length(2, 20);
            RuleFor(x => x.Model.DateOfBirth).NotEmpty();
        }
    }
}
