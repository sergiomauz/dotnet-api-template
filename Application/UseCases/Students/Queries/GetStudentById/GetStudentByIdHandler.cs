using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Students.Queries.GetStudentById
{
    public class GetStudentByIdHandler :
        IRequestHandler<GetStudentByIdQuery, GetStudentByIdVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetStudentByIdHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _studentsRepository;

        public GetStudentByIdHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetStudentByIdHandler> logger,
            IMapper mapper,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _studentsRepository = studentsRepository;
        }

        public async Task<GetStudentByIdVm> Handle(GetStudentByIdQuery query, CancellationToken cancellationToken)
        {
            // Get data by ID, if it fails throw exception
            var data = await _studentsRepository.GetByIdAsync(query.Id.Value);
            if (data == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetStudentByIdContent00001);
                var errorMessageArgs = new string[] { query.Id.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Map result to response
            var response = _mapper.Map<GetStudentByIdVm>(data);

            //
            return response;
        }
    }
}
