using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdQuery :
        IdQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<GetTeacherByIdRoute>,
        IRequest<GetTeacherByIdVm>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, GetTeacherByIdQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<GetTeacherByIdRoute, GetTeacherByIdQuery>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id));
        }
    }
}
