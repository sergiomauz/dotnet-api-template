using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Courses.Queries.SearchCoursesByTextFilter
{
    public class SearchCoursesByTextFilterQuery :
        BasicSearchQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<SearchCoursesByTextFilterRequestParams>,
        IRequest<PaginatedVm<SearchCoursesByTextFilterVm>>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, SearchCoursesByTextFilterQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<SearchCoursesByTextFilterRequestParams, SearchCoursesByTextFilterQuery>()
                .ForMember(d => d.TextFilter, m => m.MapFrom(o => o.TextFilter))
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
