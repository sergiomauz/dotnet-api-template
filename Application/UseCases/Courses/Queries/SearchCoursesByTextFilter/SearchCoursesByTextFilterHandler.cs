using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.VMs;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Courses.Queries.SearchCoursesByTextFilter
{
    public class SearchCoursesByTextFilterHandler :
        IRequestHandler<SearchCoursesByTextFilterQuery, PaginatedVm<SearchCoursesByTextFilterVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<SearchCoursesByTextFilterHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;

        public SearchCoursesByTextFilterHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<SearchCoursesByTextFilterHandler> logger,
            IMapper mapper,
            ICoursesRepository coursesRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _coursesRepository = coursesRepository;
        }

        public async Task<PaginatedVm<SearchCoursesByTextFilterVm>> Handle(SearchCoursesByTextFilterQuery query, CancellationToken cancellationToken)
        {
            // Get results
            var dataList = await _coursesRepository.SearchCoursesByTextFilterAsync(query.TextFilter,
                                                                                     query.CurrentPage.Value,
                                                                                     query.PageSize.Value);
            var totalCount = await _coursesRepository.TotalCountCoursesByTextFilterAsync(query.TextFilter);

            // Map result to response
            var items = _mapper.Map<IEnumerable<Course>, IEnumerable<SearchCoursesByTextFilterVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<SearchCoursesByTextFilterVm>(
                    items,
                    totalCount,
                    query.CurrentPage.Value,
                    query.PageSize.Value
                );

            //
            return response;
        }
    }
}
