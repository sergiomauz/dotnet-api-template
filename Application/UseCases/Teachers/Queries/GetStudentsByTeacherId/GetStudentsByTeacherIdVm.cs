using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;
using Application.Commons.Mapping;


namespace Application.UseCases.Teachers.Queries.GetStudentsByTeacherId
{
    public class GetStudentsByTeacherIdVm : IMapFrom<Enrollment>
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("student")]
        public string Student { get; set; }

        [JsonPropertyName("course")]
        public string Course { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Enrollment, GetStudentsByTeacherIdVm>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Student.Code))
                .ForMember(d => d.Student, m => m.MapFrom(o => $"{o.Student.Lastname}, {o.Student.Firstname}"))
                .ForMember(d => d.Course, m => m.MapFrom(o => o.Course.Name));
        }
    }
}
