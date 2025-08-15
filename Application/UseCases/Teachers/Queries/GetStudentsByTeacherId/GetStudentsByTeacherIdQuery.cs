using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Teachers.Queries.GetStudentsByTeacherId
{
    public class GetStudentsByTeacherIdQuery :
        PaginatedQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<GetStudentsByTeacherIdRoute>,
        IMapFrom<GetStudentsByTeacherIdRequestParams>,
        IRequest<PaginatedVm<GetStudentsByTeacherIdVm>>
    {
        public int? TeacherId { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, GetStudentsByTeacherIdQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<GetStudentsByTeacherIdRoute, GetStudentsByTeacherIdQuery>()
                .ForMember(d => d.TeacherId, m => m.MapFrom(o => o.Id));

            profile.CreateMap<GetStudentsByTeacherIdRequestParams, GetStudentsByTeacherIdQuery>()
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
