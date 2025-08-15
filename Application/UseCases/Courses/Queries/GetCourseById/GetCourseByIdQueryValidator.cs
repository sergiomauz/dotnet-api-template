using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQueryValidator : AbstractValidator<GetCourseByIdQuery>
    {
        public GetCourseByIdQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new IdQueryValidator(errorCatalogService));
        }
    }
}
