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


namespace Application.UseCases.Teachers.Queries.SearchTeachersByObject
{
    public class SearchTeachersByObjectHandler :
        IRequestHandler<SearchTeachersByObjectQuery, PaginatedVm<SearchTeachersByObjectVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<SearchTeachersByObjectHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITeachersRepository _teachersRepository;

        public SearchTeachersByObjectHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<SearchTeachersByObjectHandler> logger,
            IMapper mapper,
            ITeachersRepository teachersRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _teachersRepository = teachersRepository;
        }

        private TeachersQueryFilter? _buildTeacherFilteringCriteria(SearchTeachersByObjectQuery query)
        {
            return query.FilteringCriteria != null ? new TeachersQueryFilter
            {
                Code = query.FilteringCriteria.Code != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.Code.Operator).Value,
                    Value = query.FilteringCriteria.Code.Operand
                } : null,
                Firstname = query.FilteringCriteria.Firstname != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.Firstname.Operator).Value,
                    Value = query.FilteringCriteria.Firstname.Operand
                } : null,
                Lastname = query.FilteringCriteria.Lastname != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.Lastname.Operator).Value,
                    Value = query.FilteringCriteria.Lastname.Operand
                } : null,
                CreatedAt = query.FilteringCriteria.CreatedAt != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.CreatedAt.Operator).Value,
                    Value = query.FilteringCriteria.CreatedAt.Operand
                } : null
            } : null;
        }

        private TeachersQueryOrder? _buildTeacherOrderingCriteria(SearchTeachersByObjectQuery query)
        {
            return query.OrderingCriteria != null ? new TeachersQueryOrder
            {
                Code = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.Code),
                Firstname = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.Firstname),
                Lastname = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.Lastname),
                CreatedAt = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.CreatedAt)
            } : null;
        }

        public async Task<PaginatedVm<SearchTeachersByObjectVm>> Handle(SearchTeachersByObjectQuery query, CancellationToken cancellationToken)
        {
            // Build query
            var teacherFilteringCriteria = _buildTeacherFilteringCriteria(query);
            var teacherOrderingCriteria = _buildTeacherOrderingCriteria(query);

            // Get results
            var dataList = await _teachersRepository.SearchTeachersByObjectAsync(
                new TeachersPaginatedQuery
                {
                    FilteringCriteria = teacherFilteringCriteria,
                    OrderingCriteria = teacherOrderingCriteria,
                    CurrentPage = query.CurrentPage,
                    PageSize = query.PageSize
                });
            var totalCount = await _teachersRepository.TotalCountTeachersByObjectAsync(
                new TeachersQuery
                {
                    FilteringCriteria = teacherFilteringCriteria
                });

            // Map result to response
            var items = _mapper.Map<IEnumerable<Teacher>, IEnumerable<SearchTeachersByObjectVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<SearchTeachersByObjectVm>(
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
