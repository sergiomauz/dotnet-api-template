using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Students.Commands.DeleteStudents
{
    public class DeleteStudentsCommand :
        IdsQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<DeleteStudentsRoute>,
        IMapFrom<DeleteStudentsDto>,
        IRequest<DeleteStudentsVm>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, DeleteStudentsCommand>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<DeleteStudentsRoute, DeleteStudentsCommand>()
                .ForMember(d => d.Ids, m => m.MapFrom(o => new[] { o.Id }));

            profile.CreateMap<DeleteStudentsDto, DeleteStudentsCommand>()
                .ForMember(d => d.Ids, m => m.MapFrom(o => o.Ids));
        }
    }
}
