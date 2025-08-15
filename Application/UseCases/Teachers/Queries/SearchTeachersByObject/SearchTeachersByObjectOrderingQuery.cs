using AutoMapper;
using Application.Commons.Mapping;


namespace Application.UseCases.Teachers.Queries.SearchTeachersByObject
{
    public class SearchTeachersByObjectOrderingQuery :
        IMapFrom<SearchTeachersByObjectOrderingDto>
    {
        public string? Code { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SearchTeachersByObjectOrderingDto, SearchTeachersByObjectOrderingQuery>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(o => o.CreatedAt));
        }
    }
}
