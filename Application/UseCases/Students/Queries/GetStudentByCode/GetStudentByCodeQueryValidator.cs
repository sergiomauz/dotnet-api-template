using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Students.Queries.GetStudentByCode
{
    public class GetStudentByCodeQueryValidator : AbstractValidator<GetStudentByCodeQuery>
    {
        public GetStudentByCodeQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new CodeQueryValidator(errorCatalogService));
        }
    }
}
