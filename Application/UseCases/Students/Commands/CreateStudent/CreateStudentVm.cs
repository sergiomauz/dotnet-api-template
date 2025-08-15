using System.Text.Json.Serialization;
using AutoMapper;
using Domain.Entities;
using Application.Commons.Mapping;
using Application.Commons.VMs;


namespace Application.UseCases.Students.Commands.CreateStudent
{
    public class CreateStudentVm :
        BasicVm,
        IMapFrom<Student>
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("firstname")]
        public string? Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string Lastname { get; set; }

        [JsonPropertyName("birth_date")]
        public string BirthDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, CreateStudentVm>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id))
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname))
                .ForMember(d => d.BirthDate, m => m.MapFrom(o => o.BirthDate.Value.ToString("yyyy-MM-dd")))
                .ForMember(d => d.CreatedAt, m => m.MapFrom(o => o.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
