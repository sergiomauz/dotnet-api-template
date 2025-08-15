using FluentValidation;
using Application.Commons.Queries;
using Application.ErrorCatalog;


namespace Application.Commons.Validators
{
    public class GuidsQueryValidator : AbstractValidator<GuidsQuery>
    {
        public GuidsQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x.Ids)
                .NotNull()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00001).PropertyName);

            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00002).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00002).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00002).PropertyName)
                .When(x => x.Ids != null);

            RuleForEach(x => x.Ids)
                .Must(v => Guid.TryParse(v, out _))
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00003).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00003).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00003).PropertyName)
                .When(x => x.Ids != null && x.Ids.Count > 0);

            RuleFor(x => x.Ids)
                .Must(x => x.Count() == x.Distinct().Count())
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00004).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00004).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GuidsFormat00004).PropertyName)
                .When(x => x.Ids != null && x.Ids.Count > 0);
        }
    }
}
