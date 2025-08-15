using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Courses.Queries.SearchCoursesByObject
{
    public class SearchCoursesByObjectQuery :
        PaginatedQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<SearchCoursesByObjectDto>,
        IRequest<PaginatedVm<SearchCoursesByObjectVm>>
    {
        public SearchCoursesByObjectFilteringQuery? FilteringCriteria { get; set; }
        public SearchCoursesByObjectOrderingQuery? OrderingCriteria { get; set; }
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, SearchCoursesByObjectQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<SearchCoursesByObjectDto, SearchCoursesByObjectQuery>()
                .ForMember(d => d.FilteringCriteria, m => m.MapFrom(o => o.FilteringCriteria))
                .ForMember(d => d.OrderingCriteria, m => m.MapFrom(o => o.OrderingCriteria))
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
