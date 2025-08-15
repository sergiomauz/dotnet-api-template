using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;
using Application.Commons.VMs;


namespace Application.UseCases.Teachers.Queries.GetStudentsByTeacherId
{
    public class GetStudentsByTeacherIdHandler :
        IRequestHandler<GetStudentsByTeacherIdQuery, PaginatedVm<GetStudentsByTeacherIdVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetStudentsByTeacherIdHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IEnrollmentsRepository _enrollmentsRepository;
        private readonly ITeachersRepository _teacherRepository;

        public GetStudentsByTeacherIdHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetStudentsByTeacherIdHandler> logger,
            IMapper mapper,
            IEnrollmentsRepository enrollmentsRepository,
            ITeachersRepository teacherRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _enrollmentsRepository = enrollmentsRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<PaginatedVm<GetStudentsByTeacherIdVm>> Handle(GetStudentsByTeacherIdQuery query, CancellationToken cancellationToken)
        {
            // Verify if teacher exists
            var dataStudent = await _teacherRepository.GetByIdAsync(query.TeacherId.Value);
            if (dataStudent == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentsByTeacherIdContent00001);
                var errorMessageArgs = new string[] { query.TeacherId.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Get results
            var dataList = await _enrollmentsRepository.GetStudentsByTeacherIdAsync(query.TeacherId.Value,
                                                                                     query.CurrentPage.Value,
                                                                                     query.PageSize.Value);
            var totalCount = await _enrollmentsRepository.TotalCountStudentsByTeacherIdAsync(query.TeacherId.Value);

            // Map result to response
            var items = _mapper.Map<IEnumerable<Enrollment>, IEnumerable<GetStudentsByTeacherIdVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<GetStudentsByTeacherIdVm>(
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
