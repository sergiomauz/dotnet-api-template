using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommand :
        IdQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<UpdateStudentDto>,
        IMapFrom<UpdateStudentRoute>,
        IRequest<UpdateStudentVm>
    {
        public string? Code { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? BirthDate { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, UpdateStudentCommand>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<UpdateStudentRoute, UpdateStudentCommand>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id));

            profile.CreateMap<UpdateStudentDto, UpdateStudentCommand>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Firstname, m => m.MapFrom(o => o.Firstname))
                .ForMember(d => d.Lastname, m => m.MapFrom(o => o.Lastname))
                .ForMember(d => d.BirthDate, m => m.MapFrom(o => o.BirthDate));
        }
    }
}
