using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;
using Application.Commons.VMs;


namespace Application.UseCases.Students.Queries.GetCoursesByStudentId
{
    public class GetCoursesByStudentIdHandler :
        IRequestHandler<GetCoursesByStudentIdQuery, PaginatedVm<GetCoursesByStudentIdVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetCoursesByStudentIdHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IEnrollmentsRepository _enrollmentsRepository;
        private readonly IStudentsRepository _studentsRepository;

        public GetCoursesByStudentIdHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetCoursesByStudentIdHandler> logger,
            IMapper mapper,
            IEnrollmentsRepository enrollmentsRepository,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _enrollmentsRepository = enrollmentsRepository;
            _studentsRepository = studentsRepository;
        }

        public async Task<PaginatedVm<GetCoursesByStudentIdVm>> Handle(GetCoursesByStudentIdQuery query, CancellationToken cancellationToken)
        {
            // Verify if student exists
            var dataStudent = await _studentsRepository.GetByIdAsync(query.StudentId.Value);
            if (dataStudent == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetCoursesByStudentIdContent00001);
                var errorMessageArgs = new string[] { query.StudentId.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Get results
            var dataList = await _enrollmentsRepository.GetCoursesByStudentIdAsync(query.StudentId.Value,
                                                                                    query.CurrentPage.Value,
                                                                                    query.PageSize.Value);
            var totalCount = await _enrollmentsRepository.TotalCountCoursesByStudentIdAsync(query.StudentId.Value);

            // Map result to response
            var items = _mapper.Map<IEnumerable<Enrollment>, IEnumerable<GetCoursesByStudentIdVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<GetCoursesByStudentIdVm>(
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
