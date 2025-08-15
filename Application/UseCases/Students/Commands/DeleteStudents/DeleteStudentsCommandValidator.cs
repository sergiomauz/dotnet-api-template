using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Students.Commands.DeleteStudents
{
    public class DeleteStudentsCommandValidator : AbstractValidator<DeleteStudentsCommand>
    {
        public DeleteStudentsCommandValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new IdsQueryValidator(errorCatalogService));
        }
    }
}
