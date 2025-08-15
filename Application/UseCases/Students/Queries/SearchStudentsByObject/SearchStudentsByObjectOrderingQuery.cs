using AutoMapper;
using Application.Commons.Mapping;


namespace Application.UseCases.Students.Queries.SearchStudentsByObject
{
    public class SearchStudentsByObjectOrderingQuery :
        IMapFrom<SearchStudentsByObjectOrderingDto>
    {
        public string? Code { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? BirthDate { get; set; }
        public string? CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SearchStudentsByObjectOrderingDto, SearchStudentsByObjectOrderingQuery>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname))
                .ForMember(d => d.BirthDate, m => m.MapFrom(o => o.BirthDate))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(o => o.CreatedAt));
        }
    }
}
