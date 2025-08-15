using System.Globalization;
using FluentValidation;
using Application.ErrorCatalog;


namespace Application.UseCases.Students.Commands.CreateStudent
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00001).PropertyName);
            RuleFor(x => x.Code)
                .Length(3, 10)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00002).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00002).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00002).PropertyName)
                .When(x => !string.IsNullOrEmpty(x.Code));

            RuleFor(x => x.Firstname)
                .NotEmpty()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00003).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00003).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00003).PropertyName);
            RuleFor(x => x.Firstname)
                .Length(3, 100)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00004).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00004).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00004).PropertyName)
                .When(x => !string.IsNullOrEmpty(x.Firstname));

            RuleFor(x => x.Lastname)
                .NotEmpty()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00005).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00005).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00005).PropertyName);
            RuleFor(x => x.Lastname)
                .Length(3, 100)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00006).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00006).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00006).PropertyName)
                .When(x => !string.IsNullOrEmpty(x.Lastname));

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00007).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00007).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00007).PropertyName);
            RuleFor(x => x.BirthDate)
                .Must(v => DateTime.TryParseExact(v, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00008).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00008).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentFormat00008).PropertyName)
                .When(x => !string.IsNullOrEmpty(x.BirthDate));
        }
    }
}
