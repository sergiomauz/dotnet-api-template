using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Students.Queries.GetCoursesByStudentId
{
    public class GetCoursesByStudentIdQuery :
        PaginatedQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<GetCoursesByStudentIdRoute>,
        IMapFrom<GetCoursesByStudentIdRequestParams>,
        IRequest<PaginatedVm<GetCoursesByStudentIdVm>>
    {
        public int? StudentId { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, GetCoursesByStudentIdQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<GetCoursesByStudentIdRoute, GetCoursesByStudentIdQuery>()
                .ForMember(d => d.StudentId, m => m.MapFrom(o => o.Id));

            profile.CreateMap<GetCoursesByStudentIdRequestParams, GetCoursesByStudentIdQuery>()
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
