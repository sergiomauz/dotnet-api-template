using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Teachers.Queries.GetStudentsByTeacherId
{
    public class GetStudentsByTeacherIdQueryValidator : AbstractValidator<GetStudentsByTeacherIdQuery>
    {
        public GetStudentsByTeacherIdQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new PaginatedQueryValidator(errorCatalogService));

            RuleFor(x => x.TeacherId)
                .NotNull()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GetCoursesByTeacherIdFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GetCoursesByTeacherIdFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GetCoursesByTeacherIdFormat00001).PropertyName);
            RuleFor(x => x.TeacherId)
                .GreaterThan(0)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GetCoursesByTeacherIdFormat00002).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GetCoursesByTeacherIdFormat00002).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GetCoursesByTeacherIdFormat00002).PropertyName)
                .When(x => x.TeacherId != null);
        }
    }
}
