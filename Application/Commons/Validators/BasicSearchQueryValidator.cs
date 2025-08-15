using FluentValidation;
using Application.Commons.Queries;
using Application.ErrorCatalog;


namespace Application.Commons.Validators
{
    public class BasicSearchQueryValidator : AbstractValidator<BasicSearchQuery>
    {
        public BasicSearchQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new PaginatedQueryValidator(errorCatalogService));

            RuleFor(x => x.TextFilter)
                .Length(3, 100)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.BasicSearchFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.BasicSearchFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.BasicSearchFormat00001).PropertyName)
                .When(x => !string.IsNullOrEmpty(x.TextFilter));
        }
    }
}
