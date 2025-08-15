using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Courses.Commands.DeleteCourses
{
    public class DeleteCoursesCommandValidator : AbstractValidator<DeleteCoursesCommand>
    {
        public DeleteCoursesCommandValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new IdsQueryValidator(errorCatalogService));
        }
    }
}
