using FluentValidation;
using Application.Commons.Queries;
using Application.ErrorCatalog;


namespace Application.Commons.Validators
{
    public class IdsQueryValidator : AbstractValidator<IdsQuery>
    {
        public IdsQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x.Ids)
                .NotNull()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00001).PropertyName);

            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00002).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00002).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00002).PropertyName)
                .When(x => x.Ids != null);

            RuleForEach(x => x.Ids)
                .GreaterThan(0)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00003).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00003).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00003).PropertyName)
                .When(x => x.Ids != null && x.Ids.Count > 0);

            RuleFor(x => x.Ids)
                .Must(x => x.Count() == x.Distinct().Count())
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00004).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00004).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.IdsFormat00004).PropertyName)
                .When(x => x.Ids != null && x.Ids.Count > 0);
        }
    }
}
