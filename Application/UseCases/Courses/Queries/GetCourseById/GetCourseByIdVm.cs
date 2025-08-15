using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;
using Application.Commons.Mapping;
using Application.Commons.VMs;


namespace Application.UseCases.Courses.Queries.GetCourseById
{
    public class GetCourseByIdVm :
        BasicVm,
        IMapFrom<Course>
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, GetCourseByIdVm>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id))
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Name, m => m.MapFrom(o => o.Name))
                .ForMember(d => d.Description, m => m.MapFrom(o => o.Description))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(o => o.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(d => d.ModifiedAt, m => m.MapFrom(o => o.ModifiedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
