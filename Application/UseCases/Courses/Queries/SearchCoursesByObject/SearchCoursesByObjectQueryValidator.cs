using FluentValidation;
using Commons.Enums;
using Commons.Helpers;
using Application.Commons.Validators;
using Application.ErrorCatalog;


namespace Application.UseCases.Courses.Queries.SearchCoursesByObject
{
    public class SearchCoursesByObjectQueryValidator : AbstractValidator<SearchCoursesByObjectQuery>
    {
        public SearchCoursesByObjectQueryValidator(IErrorCatalogService errorCatalogService)
        {
            RuleFor(x => x)
                .SetValidator(new PaginatedQueryValidator(errorCatalogService));

            RuleFor(x => x.FilteringCriteria)
                .ChildRules(fc =>
                {
                    fc.RuleFor(c => c.Code)
                        .Must(v => FilteringCriterionQueryValidator.IsValid(v))
                        .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00001).ErrorCode)
                        .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00001).ErrorMessage)
                        .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00001).PropertyName)
                        .DependentRules(() =>
                        {
                            fc.RuleFor(c => c.Code.Operand)
                                .Must(v => JsonElementValidators.IsValidString(v, max: 10))
                                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00002).ErrorCode)
                                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00002).ErrorMessage)
                                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00002).PropertyName)
                                .When(c => c.Code?.Operand != null);
                        });

                    fc.RuleFor(c => c.Name)
                        .Must(v => FilteringCriterionQueryValidator.IsValid(v))
                        .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00003).ErrorCode)
                        .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00003).ErrorMessage)
                        .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00003).PropertyName)
                        .DependentRules(() =>
                        {
                            fc.RuleFor(c => c.Name.Operand)
                                .Must(v => JsonElementValidators.IsValidString(v, max: 150))
                                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00004).ErrorCode)
                                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00004).ErrorMessage)
                                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00004).PropertyName)
                                .When(c => c.Name?.Operand != null);
                        });

                    fc.RuleFor(c => c.Description)
                        .Must(v => FilteringCriterionQueryValidator.IsValid(v))
                        .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00005).ErrorCode)
                        .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00005).ErrorMessage)
                        .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00005).PropertyName)
                        .DependentRules(() =>
                        {
                            fc.RuleFor(c => c.Description.Operand)
                                .Must(v => JsonElementValidators.IsValidString(v, max: 400))
                                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00006).ErrorCode)
                                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00006).ErrorMessage)
                                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00006).PropertyName)
                                .When(c => c.Description?.Operand != null);
                        });

                    fc.RuleFor(c => c.CreatedAt)
                        .Must(v => FilteringCriterionQueryValidator.IsValid(v))
                        .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00007).ErrorCode)
                        .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00007).ErrorMessage)
                        .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00007).PropertyName)
                        .DependentRules(() =>
                        {
                            fc.RuleFor(c => c.CreatedAt.Operand)
                                .Must(v => JsonElementValidators.IsValidDateTime(v))
                                .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00008).ErrorCode)
                                .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00008).ErrorMessage)
                                .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00008).PropertyName)
                                .When(c => c.CreatedAt?.Operand != null);
                        });
                })
                .When(x => x.FilteringCriteria != null);

            RuleFor(x => x.OrderingCriteria)
                .ChildRules(oc =>
                {
                    oc.RuleFor(c => c.Code)
                        .Must(v => EnumHelper.IsValidDescription<OrderOperator>(v))
                        .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00009).ErrorCode)
                        .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00009).ErrorMessage)
                        .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00009).PropertyName)
                        .When(c => c.Code != null);

                    oc.RuleFor(c => c.Name)
                        .Must(v => EnumHelper.IsValidDescription<OrderOperator>(v))
                        .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00010).ErrorCode)
                        .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00010).ErrorMessage)
                        .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00010).PropertyName)
                        .When(c => c.Name != null);

                    oc.RuleFor(c => c.Description)
                        .Must(v => EnumHelper.IsValidDescription<OrderOperator>(v))
                        .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00011).ErrorCode)
                        .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00011).ErrorMessage)
                        .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00011).PropertyName)
                        .When(c => c.Description != null);

                    oc.RuleFor(c => c.CreatedAt)
                        .Must(v => EnumHelper.IsValidDescription<OrderOperator>(v))
                        .WithErrorCode(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00012).ErrorCode)
                        .WithMessage(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00012).ErrorMessage)
                        .OverridePropertyName(errorCatalogService.GetErrorByCode(ErrorConstants.SearchCoursesByObjectFormat00012).PropertyName)
                        .When(c => c.CreatedAt != null);
                })
                .When(x => x.OrderingCriteria != null);
        }
    }
}
