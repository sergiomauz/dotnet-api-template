using FluentValidation;
using Application.Commons.Queries;
using Application.ErrorCatalog;


namespace Application.Commons.Validators
{
    public class PaginatedQueryValidator : AbstractValidator<PaginatedQuery>
    {
        public PaginatedQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x.CurrentPage)
                .GreaterThan(0)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.PaginatedFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.PaginatedFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.PaginatedFormat00001).PropertyName)
                .When(x => x.CurrentPage != null);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.PaginatedFormat00002).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.PaginatedFormat00002).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.PaginatedFormat00002).PropertyName)
                .When(x => x.PageSize != null);
        }
    }
}
