using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Students.Queries.GetStudentByCode
{
    public class GetStudentByCodeQuery :
        CodeQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<GetStudentByCodeRoute>,
        IRequest<GetStudentByCodeVm>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, GetStudentByCodeQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<GetStudentByCodeRoute, GetStudentByCodeQuery>()
                .ForMember(d => d.Code, m => m.MapFrom(o => o.Code));
        }
    }
}
