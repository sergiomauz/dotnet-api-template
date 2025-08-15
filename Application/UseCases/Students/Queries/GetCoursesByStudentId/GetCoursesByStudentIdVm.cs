using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;
using Application.Commons.Mapping;


namespace Application.UseCases.Students.Queries.GetCoursesByStudentId
{
    public class GetCoursesByStudentIdVm : IMapFrom<Enrollment>
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("course")]
        public string Course { get; set; }

        [JsonPropertyName("teacher")]
        public string Teacher { get; set; }

        [JsonPropertyName("enrollment_date")]
        public string EnrollmentDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Enrollment, GetCoursesByStudentIdVm>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Course.Code))
                .ForMember(d => d.Course, m => m.MapFrom(o => o.Course.Name))
                .ForMember(d => d.Teacher, m => m.MapFrom(o => $"{o.Course.Teacher.Lastname}, {o.Course.Teacher.Firstname}"))
                .ForMember(d => d.EnrollmentDate, m => m.MapFrom(o => o.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
