using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Teachers.Commands.DeleteTeachers
{
    public class DeleteTeachersCommandValidator : AbstractValidator<DeleteTeachersCommand>
    {
        public DeleteTeachersCommandValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new IdsQueryValidator(errorCatalogService));
        }
    }
}
