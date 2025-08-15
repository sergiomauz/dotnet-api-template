using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Courses.Queries.GetStudentsByCourseId
{
    public class GetStudentsByCourseIdQuery :
        PaginatedQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<GetStudentsByCourseIdRoute>,
        IMapFrom<GetStudentsByCourseIdRequestParams>,
        IRequest<PaginatedVm<GetStudentsByCourseIdVm>>
    {
        public int? CourseId { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, GetStudentsByCourseIdQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<GetStudentsByCourseIdRoute, GetStudentsByCourseIdQuery>()
                .ForMember(d => d.CourseId, m => m.MapFrom(o => o.Id));

            profile.CreateMap<GetStudentsByCourseIdRequestParams, GetStudentsByCourseIdQuery>()
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
