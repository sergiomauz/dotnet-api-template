using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Courses.Queries.GetCourseById
{
    public class GetCourseByIdHandler :
        IRequestHandler<GetCourseByIdQuery, GetCourseByIdVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetCourseByIdHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;

        public GetCourseByIdHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetCourseByIdHandler> logger,
            IMapper mapper,
            ICoursesRepository coursesRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _coursesRepository = coursesRepository;
        }

        public async Task<GetCourseByIdVm> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
        {
            // Get data by ID, if it fails throw exception
            var data = await _coursesRepository.GetByIdAsync(query.Id.Value);
            if (data == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetCourseByIdContent00001);
                var errorMessageArgs = new string[] { query.Id.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Map result to response
            var response = _mapper.Map<GetCourseByIdVm>(data);

            //
            return response;
        }
    }
}
