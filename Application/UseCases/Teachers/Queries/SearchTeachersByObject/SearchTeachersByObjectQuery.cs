using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Teachers.Queries.SearchTeachersByObject
{
    public class SearchTeachersByObjectQuery :
        PaginatedQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<SearchTeachersByObjectDto>,
        IRequest<PaginatedVm<SearchTeachersByObjectVm>>
    {
        public SearchTeachersByObjectFilteringQuery? FilteringCriteria { get; set; }
        public SearchTeachersByObjectOrderingQuery? OrderingCriteria { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, SearchTeachersByObjectQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<SearchTeachersByObjectDto, SearchTeachersByObjectQuery>()
                .ForMember(d => d.FilteringCriteria, m => m.MapFrom(o => o.FilteringCriteria))
                .ForMember(d => d.OrderingCriteria, m => m.MapFrom(o => o.OrderingCriteria))
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
