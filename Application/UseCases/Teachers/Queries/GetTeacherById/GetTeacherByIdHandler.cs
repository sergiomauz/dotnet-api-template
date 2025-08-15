using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdHandler :
        IRequestHandler<GetTeacherByIdQuery, GetTeacherByIdVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<GetTeacherByIdHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITeachersRepository _teachersRepository;

        public GetTeacherByIdHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<GetTeacherByIdHandler> logger,
            IMapper mapper,
            ITeachersRepository teachersRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _teachersRepository = teachersRepository;
        }

        public async Task<GetTeacherByIdVm> Handle(GetTeacherByIdQuery query, CancellationToken cancellationToken)
        {
            // Get data by Code, if it fails throw exception
            var data = await _teachersRepository.GetByIdAsync(query.Id.Value);
            if (data == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.GetTeacherByIdContent00001);
                var errorMessageArgs = new string[] { query.Id.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Map result to response
            var response = _mapper.Map<GetTeacherByIdVm>(data);

            //
            return response;
        }
    }
}
