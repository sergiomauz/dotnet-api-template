using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.VMs;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Students.Queries.SearchStudentsByTextFilter
{
    public class SearchStudentsByTextFilterHandler :
        IRequestHandler<SearchStudentsByTextFilterQuery, PaginatedVm<SearchStudentsByTextFilterVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<SearchStudentsByTextFilterHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _studentsRepository;

        public SearchStudentsByTextFilterHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<SearchStudentsByTextFilterHandler> logger,
            IMapper mapper,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _studentsRepository = studentsRepository;
        }

        public async Task<PaginatedVm<SearchStudentsByTextFilterVm>> Handle(SearchStudentsByTextFilterQuery query, CancellationToken cancellationToken)
        {
            // Get results
            var dataList = await _studentsRepository.SearchStudentsByTextFilterAsync(query.TextFilter,
                                                                                     query.CurrentPage.Value,
                                                                                     query.PageSize.Value);
            var totalCount = await _studentsRepository.TotalCountStudentsByTextFilterAsync(query.TextFilter);

            // Map result to response
            var items = _mapper.Map<IEnumerable<Student>, IEnumerable<SearchStudentsByTextFilterVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<SearchStudentsByTextFilterVm>(
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
