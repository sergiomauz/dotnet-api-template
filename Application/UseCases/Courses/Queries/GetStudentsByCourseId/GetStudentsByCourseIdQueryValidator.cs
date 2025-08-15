using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Courses.Queries.GetStudentsByCourseId
{
    public class GetStudentsByCourseIdQueryValidator : AbstractValidator<GetStudentsByCourseIdQuery>
    {
        public GetStudentsByCourseIdQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new PaginatedQueryValidator(errorCatalogService));

            RuleFor(x => x.CourseId)
                .NotNull()
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentsByCourseIdFormat00001).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentsByCourseIdFormat00001).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentsByCourseIdFormat00001).PropertyName);
            RuleFor(x => x.CourseId)
                .GreaterThan(0)
                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentsByCourseIdFormat00002).ErrorCode)
                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentsByCourseIdFormat00002).ErrorMessage)
                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentsByCourseIdFormat00002).PropertyName)
                .When(x => x.CourseId != null);
        }
    }
}
