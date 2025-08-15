using AutoMapper;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Students.Queries.SearchStudentsByObject
{
    public class SearchStudentsByObjectFilteringQuery :
        IMapFrom<SearchStudentsByObjectFilteringDto>
    {
        public FilteringCriterionQuery? Code { get; set; }
        public FilteringCriterionQuery? Firstname { get; set; }
        public FilteringCriterionQuery? Lastname { get; set; }
        public FilteringCriterionQuery? BirthDate { get; set; }
        public FilteringCriterionQuery? CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SearchStudentsByObjectFilteringDto, SearchStudentsByObjectFilteringQuery>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname))
                .ForMember(d => d.BirthDate, m => m.MapFrom(o => o.BirthDate))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(o => o.CreatedAt));
        }
    }
}
