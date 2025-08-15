using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Students.Queries.GetStudentByCode
{
    public class GetStudentByCodeHandler :
        IRequestHandler<GetStudentByCodeQuery, GetStudentByCodeVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetStudentByCodeHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _studentsRepository;

        public GetStudentByCodeHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetStudentByCodeHandler> logger,
            IMapper mapper,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _studentsRepository = studentsRepository;
        }

        public async Task<GetStudentByCodeVm> Handle(GetStudentByCodeQuery query, CancellationToken cancellationToken)
        {
            // Get data by ID, if it fails throw exception
            var data = await _studentsRepository.GetByCodeAsync(query.Code);
            if (data == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentByCodeContent00001);
                var errorMessageArgs = new string[] { query.Code };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Map result to response
            var response = _mapper.Map<GetStudentByCodeVm>(data);

            //
            return response;
        }
    }
}
