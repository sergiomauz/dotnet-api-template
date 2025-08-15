using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand :
        IdQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<UpdateCourseRoute>,
        IMapFrom<UpdateCourseDto>,
        IRequest<UpdateCourseVm>
    {
        public int? TeacherId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, UpdateCourseCommand>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<UpdateCourseRoute, UpdateCourseCommand>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id));

            profile.CreateMap<UpdateCourseDto, UpdateCourseCommand>()
                .ForMember(d => d.TeacherId, m => m.MapFrom(o => o.TeacherId))
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code))
                .ForMember(d => d.Name, m => m.MapFrom(o => o.Name))
                .ForMember(d => d.Description, m => m.MapFrom(o => o.Description));
        }
    }
}
