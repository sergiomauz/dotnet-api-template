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


namespace Application.UseCases.Students.Queries.SearchStudentsByObject
{
    public class SearchStudentsByObjectHandler :
        IRequestHandler<SearchStudentsByObjectQuery, PaginatedVm<SearchStudentsByObjectVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<SearchStudentsByObjectHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _studentsRepository;

        public SearchStudentsByObjectHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<SearchStudentsByObjectHandler> logger,
            IMapper mapper,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _studentsRepository = studentsRepository;
        }

        private StudentsQueryFilter? _buildStudentFilteringCriteria(SearchStudentsByObjectQuery query)
        {
            return query.FilteringCriteria != null ? new StudentsQueryFilter
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
                BirthDate = query.FilteringCriteria.BirthDate != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.BirthDate.Operator).Value,
                    Value = query.FilteringCriteria.BirthDate.Operand
                } : null,
                CreatedAt = query.FilteringCriteria.CreatedAt != null ? new FilteringCriterion
                {
                    Operator = EnumHelper.FromDescription<FilterOperator>(query.FilteringCriteria.CreatedAt.Operator).Value,
                    Value = query.FilteringCriteria.CreatedAt.Operand
                } : null
            } : null;
        }

        private StudentsQueryOrder? _buildStudentOrderingCriteria(SearchStudentsByObjectQuery query)
        {
            return query.OrderingCriteria != null ? new StudentsQueryOrder
            {
                Code = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.Code),
                Firstname = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.Firstname),
                Lastname = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.Lastname),
                BirthDate = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.BirthDate),
                CreatedAt = EnumHelper.FromDescription<OrderOperator>(query.OrderingCriteria.CreatedAt)
            } : null;
        }

        public async Task<PaginatedVm<SearchStudentsByObjectVm>> Handle(SearchStudentsByObjectQuery query, CancellationToken cancellationToken)
        {
            // Build query
            var studentFilteringCriteria = _buildStudentFilteringCriteria(query);
            var studentOrderingCriteria = _buildStudentOrderingCriteria(query);

            // Get results
            var dataList = await _studentsRepository.SearchStudentsByObjectAsync(
                new StudentsPaginatedQuery
                {
                    FilteringCriteria = studentFilteringCriteria,
                    OrderingCriteria = studentOrderingCriteria,
                    CurrentPage = query.CurrentPage,
                    PageSize = query.PageSize
                });
            var totalCount = await _studentsRepository.TotalCountStudentsByObjectAsync(
                new StudentsQuery
                {
                    FilteringCriteria = studentFilteringCriteria
                });

            // Map result to response
            var items = _mapper.Map<IEnumerable<Student>, IEnumerable<SearchStudentsByObjectVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<SearchStudentsByObjectVm>(
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
