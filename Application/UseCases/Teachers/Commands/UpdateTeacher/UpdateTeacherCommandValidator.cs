using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherCommandValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new IdQueryValidator(errorCatalogService));

            RuleFor(x => x.Code)
                .Length(3, 10)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentFormat00001).PropertyName)
                .When(x => !string.IsNullOrEmpty(x.Code));

            RuleFor(x => x.Firstname)
                .Length(3, 100)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentFormat00002).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentFormat00002).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentFormat00002).PropertyName)
                .When(x => !string.IsNullOrEmpty(x.Firstname));

            RuleFor(x => x.Lastname)
                .Length(3, 100)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentFormat00003).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentFormat00003).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentFormat00003).PropertyName)
                .When(x => !string.IsNullOrEmpty(x.Lastname));
        }
    }
}
