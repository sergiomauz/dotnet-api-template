using AutoMapper;
using Commons.Enums;
using Application.Commons.Mapping;
using Application.Commons.RequestParams;


namespace Application.Commons.Queries
{
    public class FilteringCriterionQuery :
        IMapFrom<FilteringCriterionRequestParams>
    {
        public FilterOperator Operator { get; set; }
        public object? Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FilteringCriterionRequestParams, FilteringCriterionQuery>()
                .ForMember(d => d.Operator, m => m.MapFrom(o => EnumHelper.FromDescription<FilterOperator>(o.Operator)))
                .ForMember(d => d.Value, m => m.MapFrom(o => o.Value));
        }
    }
}
