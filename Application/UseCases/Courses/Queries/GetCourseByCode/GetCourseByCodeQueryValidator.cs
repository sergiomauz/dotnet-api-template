using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Courses.Queries.GetCourseByCode
{
    public class GetCourseByCodeQueryValidator : AbstractValidator<GetCourseByCodeQuery>
    {
        public GetCourseByCodeQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new CodeQueryValidator(errorCatalogService));
        }
    }
}
