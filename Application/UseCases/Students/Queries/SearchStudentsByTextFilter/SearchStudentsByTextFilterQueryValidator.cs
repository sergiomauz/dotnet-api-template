using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Students.Queries.SearchStudentsByTextFilter
{
    public class SearchStudentsByTextFilterQueryValidator : AbstractValidator<SearchStudentsByTextFilterQuery>
    {
        public SearchStudentsByTextFilterQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new BasicSearchQueryValidator(errorCatalogService));
        }
    }
}
