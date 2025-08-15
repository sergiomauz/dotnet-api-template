using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;


namespace Application.UseCases.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommand :
        IMapFrom<HttpRequest>,
        IMapFrom<CreateTeacherDto>,
        IRequest<CreateTeacherVm>
    {
        public string? Code { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, CreateTeacherCommand>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<CreateTeacherDto, CreateTeacherCommand>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname));
        }
    }
}
