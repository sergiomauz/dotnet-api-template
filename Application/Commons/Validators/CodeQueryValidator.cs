using FluentValidation;
using Application.Commons.Queries;
using Application.ErrorCatalog;


namespace Application.Commons.Validators
{
    public class CodeQueryValidator : AbstractValidator<CodeQuery>
    {
        public CodeQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x.Code)
                .Length(3, 25)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.CodeFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.CodeFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.CodeFormat00001).PropertyName)
                .When(x => !string.IsNullOrEmpty(x.Code));
        }
    }
}
