using FluentValidation;
using Application.Commons.Queries;
using Application.ErrorCatalog;


namespace Application.Commons.Validators
{
    public class IdsQueryValidator :
        AbstractValidator<IdsQuery>
    {
        public IdsQueryValidator(IErrorCatalogService errorCatalogService)
        {

        }
    }
}
