using FluentValidation;
using Application.Commons.Queries;
using Application.ErrorCatalog;


namespace Application.Commons.Validators
{
    public class IdQueryValidator : AbstractValidator<IdQuery>
    {
        public IdQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.IdFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.IdFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.IdFormat00001).PropertyName);

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.IdFormat00002).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.IdFormat00002).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.IdFormat00002).PropertyName)
                .When(x => x.Id != null);
        }
    }
}
