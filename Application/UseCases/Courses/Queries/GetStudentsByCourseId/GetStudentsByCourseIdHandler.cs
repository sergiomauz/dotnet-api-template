using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.Exceptions;
using Application.Commons.VMs;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Courses.Queries.GetStudentsByCourseId
{
    public class GetStudentsByCourseIdHandler :
        IRequestHandler<GetStudentsByCourseIdQuery, PaginatedVm<GetStudentsByCourseIdVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetStudentsByCourseIdHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IStudentsRepository _studentsRepository;

        public GetStudentsByCourseIdHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetStudentsByCourseIdHandler> logger,
            IMapper mapper,
            ICoursesRepository coursesRepository,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _coursesRepository = coursesRepository;
            _studentsRepository = studentsRepository;
        }

        public async Task<PaginatedVm<GetStudentsByCourseIdVm>> Handle(GetStudentsByCourseIdQuery query, CancellationToken cancellationToken)
        {
            // Verify if course exists
            var dataCourse = await _coursesRepository.GetByIdAsync(query.CourseId.Value);
            if (dataCourse == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentsByCourseIdContent00001);
                var errorMessageArgs = new string[] { query.CourseId.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Get results
            var dataList = await _studentsRepository.GetStudentsByCourseIdAsync(query.CourseId.Value,
                                                                                 query.CurrentPage.Value,
                                                                                 query.PageSize.Value);
            var totalCount = await _studentsRepository.TotalStudentsByCourseIdAsync(Convert.ToInt32(query.CourseId));

            // Map result to response
            var items = _mapper.Map<IEnumerable<Enrollment>, IEnumerable<GetStudentsByCourseIdVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<GetStudentsByCourseIdVm>(
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
