using FluentValidation;
using Application.Commons.Queries;
using Application.ErrorCatalog;


namespace Application.Commons.Validators
{
    public class GuidQueryValidator : AbstractValidator<GuidQuery>
    {
        public GuidQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GuidFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GuidFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GuidFormat00001).PropertyName);

            RuleFor(x => x.Id)
                .Must(v => Guid.TryParse(v, out _))
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GuidFormat00002).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GuidFormat00002).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GuidFormat00002).PropertyName)
                .When(x => x.Id != null);
        }
    }
}
