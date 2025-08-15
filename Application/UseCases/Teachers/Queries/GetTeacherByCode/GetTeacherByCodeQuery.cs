using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Teachers.Queries.GetTeacherByCode
{
    public class GetTeacherByCodeQuery :
        CodeQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<GetTeacherByCodeRoute>,
        IRequest<GetTeacherByCodeVm>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, GetTeacherByCodeQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<GetTeacherByCodeRoute, GetTeacherByCodeQuery>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code));
        }
    }
}
