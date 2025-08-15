using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Commons.Enums;
using Domain.Entities;
using Domain.QueryObjects;
using Domain.QueryObjects.Utils;
using Application.Commons.VMs;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Courses.Queries.SearchCoursesByObject
{
    public class SearchCoursesByObjectHandler :
        IRequestHandler<SearchCoursesByObjectQuery, PaginatedVm<SearchCoursesByObjectVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<SearchCoursesByObjectHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;

        public SearchCoursesByObjectHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<SearchCoursesByObjectHandler> logger,
            IMapper mapper,
            ICoursesRepository coursesRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _coursesRepository = coursesRepository;
        }

        private CoursesQueryFilter? _buildCourseFilteringCriteria(SearchCoursesByObjectQuery query)
        {
            return query.FilteringCriteria != null ? new CoursesQueryFilter
            {
                Code = query.FilteringCriteria.Code != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.Code.Operator).Value,
                    Value = query.FilteringCriteria.Code.Operand
                } : null,
                Name = query.FilteringCriteria.Name != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.Name.Operator).Value,
                    Value = query.FilteringCriteria.Name.Operand
                } : null,
                Description = query.FilteringCriteria.Description != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.Description.Operator).Value,
                    Value = query.FilteringCriteria.Description.Operand
                } : null,
                CreatedAt = query.FilteringCriteria.CreatedAt != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.CreatedAt.Operator).Value,
                    Value = query.FilteringCriteria.CreatedAt.Operand
                } : null
            } : null;
        }

        private CoursesQueryOrder? _buildCourseOrderingCriteria(SearchCoursesByObjectQuery query)
        {
            return query.OrderingCriteria != null ? new CoursesQueryOrder
            {
                Code = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.Code),
                Name = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.Name),
                Description = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.Description),
                CreatedAt = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.CreatedAt)
            } : null;
        }

        public async Task<PaginatedVm<SearchCoursesByObjectVm>> Handle(SearchCoursesByObjectQuery query, CancellationToken cancellationToken)
        {
            // Build query
            var courseFilteringCriteria = _buildCourseFilteringCriteria(query);
            var courseOrderingCriteria = _buildCourseOrderingCriteria(query);

            // Get results                            
            var dataList = await _coursesRepository.SearchCoursesByObjectAsync(
                new CoursesPaginatedQuery
                {
                    FilteringCriteria = courseFilteringCriteria,
                    OrderingCriteria = courseOrderingCriteria,
                    CurrentPage = query.CurrentPage,
                    PageSize = query.PageSize
                });
            var totalCount = await _coursesRepository.TotalCountCoursesByObjectAsync(
                new CoursesQuery
                {
                    FilteringCriteria = courseFilteringCriteria
                });

            // Map result to response
            var items = _mapper.Map<IEnumerable<Course>, IEnumerable<SearchCoursesByObjectVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<SearchCoursesByObjectVm>(
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
