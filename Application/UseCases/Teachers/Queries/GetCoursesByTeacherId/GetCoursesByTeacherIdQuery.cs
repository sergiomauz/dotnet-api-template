using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Teachers.Queries.GetCoursesByTeacherId
{
    public class GetCoursesByTeacherIdQuery :
        PaginatedQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<GetCoursesByTeacherIdRoute>,
        IMapFrom<GetCoursesByTeacherIdRequestParams>,
        IRequest<PaginatedVm<GetCoursesByTeacherIdVm>>
    {
        public int? TeacherId { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, GetCoursesByTeacherIdQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<GetCoursesByTeacherIdRoute, GetCoursesByTeacherIdQuery>()
                .ForMember(d => d.TeacherId, m => m.MapFrom(o => o.Id));

            profile.CreateMap<GetCoursesByTeacherIdRequestParams, GetCoursesByTeacherIdQuery>()
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
