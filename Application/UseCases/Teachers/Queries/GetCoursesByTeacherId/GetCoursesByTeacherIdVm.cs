using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;
using Application.Commons.Mapping;


namespace Application.UseCases.Teachers.Queries.GetCoursesByTeacherId
{
    public class GetCoursesByTeacherIdVm : IMapFrom<Course>
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("course")]
        public string Course { get; set; }

        [JsonPropertyName("students")]
        public int Students { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, GetCoursesByTeacherIdVm>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Course, m => m.MapFrom(o => o.Name))
                .ForMember(d => d.Students, m => m.MapFrom(o => o.NotMappedStudents.Value));
        }
    }
}
