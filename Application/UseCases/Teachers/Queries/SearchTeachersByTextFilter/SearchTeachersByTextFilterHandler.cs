using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;
using Application.Commons.VMs;


namespace Application.UseCases.Teachers.Queries.SearchTeachersByTextFilter
{
    public class SearchTeachersByTextFilterHandler :
        IRequestHandler<SearchTeachersByTextFilterQuery, PaginatedVm<SearchTeachersByTextFilterVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<SearchTeachersByTextFilterHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITeachersRepository _teachersRepository;

        public SearchTeachersByTextFilterHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<SearchTeachersByTextFilterHandler> logger,
            IMapper mapper,
            ITeachersRepository teachersRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _teachersRepository = teachersRepository;
        }

        public async Task<PaginatedVm<SearchTeachersByTextFilterVm>> Handle(SearchTeachersByTextFilterQuery query, CancellationToken cancellationToken)
        {
            // Get results
            var dataList = await _teachersRepository.SearchTeachersByTextFilterAsync(query.TextFilter,
                                                                                     query.CurrentPage.Value,
                                                                                     query.PageSize.Value);
            var totalCount = await _teachersRepository.TotalCountTeachersByTextFilterAsync(query.TextFilter);

            // Map result to response
            var items = _mapper.Map<IEnumerable<Teacher>, IEnumerable<SearchTeachersByTextFilterVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<SearchTeachersByTextFilterVm>(
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
