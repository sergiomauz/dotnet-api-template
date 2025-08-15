using Microsoft.AspNetCore.Http;
using AutoMapper;
using MediatR;
using Application.Commons.Mapping;
using Application.Commons.Queries;
using Application.Commons.VMs;


namespace Application.UseCases.Students.Queries.SearchStudentsByTextFilter
{
    public class SearchStudentsByTextFilterQuery :
        BasicSearchQuery,
        IMapFrom<HttpRequest>,
        IMapFrom<SearchStudentsByTextFilterRequestParams>,
        IRequest<PaginatedVm<SearchStudentsByTextFilterVm>>
    {
        public HttpRequest? Request { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HttpRequest, SearchStudentsByTextFilterQuery>()
                .ForMember(d => d.Request, m => m.MapFrom(o => o));

            profile.CreateMap<SearchStudentsByTextFilterRequestParams, SearchStudentsByTextFilterQuery>()
                .ForMember(d => d.TextFilter, m => m.MapFrom(o => o.TextFilter))
                .ForMember(d => d.CurrentPage, m => m.MapFrom(o => o.CurrentPage ?? 1))
                .ForMember(d => d.PageSize, m => m.MapFrom(o => o.PageSize ?? 20));
        }
    }
}
