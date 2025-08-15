using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;
using Application.Commons.Mapping;
using Application.Commons.VMs;


namespace Application.UseCases.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentVm :
        BasicVm,
        IMapFrom<Enrollment>
    {
        [JsonPropertyName("course_id")]
        public int CourseId { get; set; }

        [JsonPropertyName("student_id")]
        public int StudentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Enrollment, CreateEnrollmentVm>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id))
                .ForMember(d => d.CourseId, m => m.MapFrom(o => o.CourseId))
                .ForMember(d => d.StudentId, m => m.MapFrom(o => o.StudentId))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(o => o.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
