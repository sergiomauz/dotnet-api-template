using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;
using Application.Commons.Mapping;


namespace Application.UseCases.Courses.Queries.GetStudentsByCourseId
{
    public class GetStudentsByCourseIdVm :
        IMapFrom<Enrollment>
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string Lastname { get; set; }

        [JsonPropertyName("enrollment_date")]
        public string EnrollmentDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Enrollment, GetStudentsByCourseIdVm>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Student.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Student.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Student.Lastname))
                .ForMember(d => d.EnrollmentDate, m => m.MapFrom(o => o.CreatedAt.Value.ToString("yyyy-MM-dd")));
        }
    }
}
