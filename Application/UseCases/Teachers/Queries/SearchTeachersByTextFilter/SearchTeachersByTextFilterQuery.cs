using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Teachers.Queries.SearchTeachersByTextFilter
{
    public class SearchTeachersByTextFilterQuery :
        BasicSearchQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<SearchTeachersByTextFilterRequestParams>,
        IRequest<PaginatedVm<SearchTeachersByTextFilterVm>>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, SearchTeachersByTextFilterQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<SearchTeachersByTextFilterRequestParams, SearchTeachersByTextFilterQuery>()
                .ForMember(d => d.TextFilter, m => m.MapFrom(o => o.TextFilter))
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
