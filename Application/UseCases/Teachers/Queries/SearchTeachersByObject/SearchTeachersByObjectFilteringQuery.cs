using AutoMapper;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Teachers.Queries.SearchTeachersByObject
{
    public class SearchTeachersByObjectFilteringQuery :
        IMapFrom<SearchTeachersByObjectFilteringDto>
    {
        public FilteringCriterionQuery? Code { get; set; }
        public FilteringCriterionQuery? Firstname { get; set; }
        public FilteringCriterionQuery? Lastname { get; set; }
        public FilteringCriterionQuery? CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SearchTeachersByObjectFilteringDto, SearchTeachersByObjectFilteringQuery>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(o => o.CreatedAt));
        }
    }
}
