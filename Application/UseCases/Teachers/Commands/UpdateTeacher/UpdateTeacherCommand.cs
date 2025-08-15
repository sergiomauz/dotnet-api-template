using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommand :
        IdQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<UpdateTeacherDto>,
        IMapFrom<UpdateTeacherRoute>,
        IRequest<UpdateTeacherVm>
    {
        public string? Code { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, UpdateTeacherCommand>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<UpdateTeacherRoute, UpdateTeacherCommand>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id));

            profile.CreateMap<UpdateTeacherDto, UpdateTeacherCommand>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname));
        }
    }
}
