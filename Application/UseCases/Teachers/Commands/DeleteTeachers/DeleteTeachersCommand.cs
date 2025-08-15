using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;


namespace Application.UseCases.Teachers.Commands.DeleteTeachers
{
    public class DeleteTeachersCommand :
        IdsQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<DeleteTeachersRoute>,
        IMapFrom<DeleteTeachersDto>,
        IRequest<DeleteTeachersVm>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, DeleteTeachersCommand>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<DeleteTeachersRoute, DeleteTeachersCommand>()
                .ForMember(d => d.Ids, m => m.MapFrom(o => new[] { o.Id }));

            profile.CreateMap<DeleteTeachersDto, DeleteTeachersCommand>()
                .ForMember(d => d.Ids, m => m.MapFrom(o => o.Ids));
        }
    }
}
