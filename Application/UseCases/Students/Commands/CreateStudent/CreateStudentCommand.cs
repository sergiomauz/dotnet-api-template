using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;


namespace Application.UseCases.Students.Commands.CreateStudent
{
    public class CreateStudentCommand :
        IMapFrom<HttpRequest>,
        IMapFrom<CreateStudentDto>,
        IRequest<CreateStudentVm>
    {
        public string Code { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string BirthDate { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, CreateStudentCommand>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<CreateStudentDto, CreateStudentCommand>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname))
                .ForMember(d => d.BirthDate, m => m.MapFrom(o => o.BirthDate));
        }
    }
}
