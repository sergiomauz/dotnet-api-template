using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Students.Queries.SearchStudentsByObject
{
    public class SearchStudentsByObjectQuery :
        PaginatedQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<SearchStudentsByObjectDto>,
        IRequest<PaginatedVm<SearchStudentsByObjectVm>>
    {
        public SearchStudentsByObjectFilteringQuery? FilteringCriteria { get; set; }
        public SearchStudentsByObjectOrderingQuery? OrderingCriteria { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, SearchStudentsByObjectQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<SearchStudentsByObjectDto, SearchStudentsByObjectQuery>()
                .ForMember(d => d.FilteringCriteria, m => m.MapFrom(o => o.FilteringCriteria))
                .ForMember(d => d.OrderingCriteria, m => m.MapFrom(o => o.OrderingCriteria))
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
