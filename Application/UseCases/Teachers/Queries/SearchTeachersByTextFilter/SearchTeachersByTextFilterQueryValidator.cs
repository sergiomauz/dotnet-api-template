using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Teachers.Queries.SearchTeachersByTextFilter
{
    public class SearchTeachersByTextFilterQueryValidator : AbstractValidator<SearchTeachersByTextFilterQuery>
    {
        public SearchTeachersByTextFilterQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new BasicSearchQueryValidator(errorCatalogService));
        }
    }
}
