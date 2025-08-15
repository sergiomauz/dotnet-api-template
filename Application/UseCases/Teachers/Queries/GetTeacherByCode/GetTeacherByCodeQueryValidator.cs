using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Teachers.Queries.GetTeacherByCode
{
    public class GetTeacherByCodeQueryValidator : AbstractValidator<GetTeacherByCodeQuery>
    {
        public GetTeacherByCodeQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new CodeQueryValidator(errorCatalogService));
        }
    }
}
