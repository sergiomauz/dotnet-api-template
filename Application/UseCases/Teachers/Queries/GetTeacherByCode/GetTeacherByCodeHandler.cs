using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Teachers.Queries.GetTeacherByCode
{
    public class GetTeacherByCodeHandler :
        IRequestHandler<GetTeacherByCodeQuery, GetTeacherByCodeVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetTeacherByCodeHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITeachersRepository _teachersRepository;

        public GetTeacherByCodeHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetTeacherByCodeHandler> logger,
            IMapper mapper,
            ITeachersRepository teachersRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _teachersRepository = teachersRepository;
        }

        public async Task<GetTeacherByCodeVm> Handle(GetTeacherByCodeQuery query, CancellationToken cancellationToken)
        {
            // Get data by ID, if it fails throw exception
            var data = await _teachersRepository.GetByCodeAsync(query.Code);
            if (data == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetTeacherByCodeContent00001);
                var errorMessageArgs = new string[] { query.Code };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Map result to response
            var response = _mapper.Map<GetTeacherByCodeVm>(data);

            //
            return response;
        }
    }
}
