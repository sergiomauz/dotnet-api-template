using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.Exceptions;
using Application.Commons.VMs;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Teachers.Queries.GetCoursesByTeacherId
{
    public class GetCoursesByTeacherIdHandler :
        IRequestHandler<GetCoursesByTeacherIdQuery, PaginatedVm<GetCoursesByTeacherIdVm>>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetCoursesByTeacherIdHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;
        private readonly ITeachersRepository _teacherRepository;

        public GetCoursesByTeacherIdHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetCoursesByTeacherIdHandler> logger,
            IMapper mapper,
            ICoursesRepository coursesRepository,
            ITeachersRepository teacherRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _coursesRepository = coursesRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<PaginatedVm<GetCoursesByTeacherIdVm>> Handle(GetCoursesByTeacherIdQuery query, CancellationToken cancellationToken)
        {
            // Verify if teacher exists
            var dataStudent = await _teacherRepository.GetByIdAsync(query.TeacherId.Value);
            if (dataStudent == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetCoursesByTeacherIdContent00001);
                var errorMessageArgs = new string[] { query.TeacherId.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Get results
            var dataList = await _coursesRepository.GetCoursesByTeacherIdAsync(query.TeacherId.Value,
                                                                                 query.CurrentPage.Value,
                                                                                 query.PageSize.Value);
            var totalCount = await _coursesRepository.TotalCoursesByTeacherIdAsync(Convert.ToInt32(query.TeacherId));

            // Map result to response
            var items = _mapper.Map<IEnumerable<Course>, IEnumerable<GetCoursesByTeacherIdVm>>(dataList);

            // Format search results
            var response = new PaginatedVm<GetCoursesByTeacherIdVm>(
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
