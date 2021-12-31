using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.Name).MinimumLength(2)
                .When(x => x.Model.Name.Trim() != string.Empty);
            
            RuleFor(x => x.Model.SurName).MinimumLength(2)
                .When(x => x.Model.SurName.Trim() != string.Empty);

            RuleFor(x => x.Model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}
