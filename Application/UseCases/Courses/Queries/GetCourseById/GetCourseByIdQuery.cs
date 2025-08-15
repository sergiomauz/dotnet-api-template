using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery :
        IdQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<GetCourseByIdRoute>,
        IRequest<GetCourseByIdVm>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, GetCourseByIdQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<GetCourseByIdRoute, GetCourseByIdQuery>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id));
        }
    }
}
