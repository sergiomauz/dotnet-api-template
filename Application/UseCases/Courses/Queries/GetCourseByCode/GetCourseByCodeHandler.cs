using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Courses.Queries.GetCourseByCode
{
    public class GetCourseByCodeHandler :
        IRequestHandler<GetCourseByCodeQuery, GetCourseByCodeVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetCourseByCodeHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;

        public GetCourseByCodeHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetCourseByCodeHandler> logger,
            IMapper mapper,
            ICoursesRepository coursesRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _coursesRepository = coursesRepository;
        }

        public async Task<GetCourseByCodeVm> Handle(GetCourseByCodeQuery query, CancellationToken cancellationToken)
        {
            // Get data by ID, if it fails throw exception
            var data = await _coursesRepository.GetByCodeAsync(query.Code);
            if (data == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetCourseByCodeContent00001);
                var errorMessageArgs = new string[] { query.Code };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Map result to response
            var response = _mapper.Map<GetCourseByCodeVm>(data);

            //
            return response;
        }
    }
}
