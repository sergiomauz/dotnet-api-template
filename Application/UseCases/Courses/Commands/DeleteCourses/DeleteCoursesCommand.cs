using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Courses.Commands.DeleteCourses
{
    public class DeleteCoursesCommand :
        IdsQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<DeleteCoursesRoute>,
        IMapFrom<DeleteCoursesDto>,
        IRequest<DeleteCoursesVm>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, DeleteCoursesCommand>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<DeleteCoursesRoute, DeleteCoursesCommand>()
                .ForMember(d => d.Ids, m => m.MapFrom(o => new[] { o.Id }));

            profile.CreateMap<DeleteCoursesDto, DeleteCoursesCommand>()
                .ForMember(d => d.Ids, m => m.MapFrom(o => o.Ids));
        }
    }
}
