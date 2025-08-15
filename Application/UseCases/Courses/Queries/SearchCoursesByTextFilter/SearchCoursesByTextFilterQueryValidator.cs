using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Courses.Queries.SearchCoursesByTextFilter
{
    public class SearchCoursesByTextFilterQueryValidator : AbstractValidator<SearchCoursesByTextFilterQuery>
    {
        public SearchCoursesByTextFilterQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new BasicSearchQueryValidator(errorCatalogService));
        }
    }
}
