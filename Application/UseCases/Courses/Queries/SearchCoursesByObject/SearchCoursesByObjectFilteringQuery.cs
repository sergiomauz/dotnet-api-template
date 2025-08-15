using AutoMapper;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Courses.Queries.SearchCoursesByObject
{
    public class SearchCoursesByObjectFilteringQuery :
        IMapFrom<SearchCoursesByObjectFilteringDto>
    {
        public FilteringCriterionQuery? Code { get; set; }
        public FilteringCriterionQuery? Name { get; set; }
        public FilteringCriterionQuery? Description { get; set; }
        public FilteringCriterionQuery? CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SearchCoursesByObjectFilteringDto, SearchCoursesByObjectFilteringQuery>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Name, m => m.MapFrom(o => o.Name))
                .ForMember(d => d.Description, m => m.MapFrom(o => o.Description))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(o => o.CreatedAt));
        }
    }
}
