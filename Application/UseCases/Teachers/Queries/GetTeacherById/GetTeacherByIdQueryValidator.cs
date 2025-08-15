using FluentValidation;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdQueryValidator : AbstractValidator<GetTeacherByIdQuery>
    {
        public GetTeacherByIdQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new IdQueryValidator(errorCatalogService));
        }
    }
}
