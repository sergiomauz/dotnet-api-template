using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;
using Application.Commons.Mapping;
using Application.Commons.VMs;


namespace Application.UseCases.Teachers.Queries.SearchTeachersByTextFilter
{
    public class SearchTeachersByTextFilterVm :
        BasicVm,
        IMapFrom<Teacher>
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string Lastname { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Teacher, SearchTeachersByTextFilterVm>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id))
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(o => o.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(d => d.ModifiedAt, m => m.MapFrom(o => o.ModifiedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
