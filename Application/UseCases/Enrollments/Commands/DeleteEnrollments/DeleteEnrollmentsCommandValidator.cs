using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Enrollments.Commands.DeleteEnrollments
{
    public class DeleteEnrollmentsCommandValidator : AbstractValidator<DeleteEnrollmentsCommand>
    {
        public DeleteEnrollmentsCommandValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new GuidsQueryValidator(errorCatalogService));
        }
    }
}
