using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherHandler :
        IRequestHandler<UpdateTeacherCommand, UpdateTeacherVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<UpdateTeacherHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITeachersRepository _teachersRepository;

        public UpdateTeacherHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<UpdateTeacherHandler> logger,
            IMapper mapper,
            ITeachersRepository teachersRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _teachersRepository = teachersRepository;
        }

        public async Task<UpdateTeacherVm> Handle(UpdateTeacherCommand command, CancellationToken cancellationToken)
        {
            // Verify if teacher exists
            var existingTeacher = await _teachersRepository.GetByIdAsync(command.Id.Value);
            if (existingTeacher == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.UpdateTeacherContent00001);
                var errorMessageArgs = new string[] { command.Id.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.NotFound);
            }

            // Verify which fields to update
            if (!string.IsNullOrEmpty(command.Code))
            {
                // Verify if a valid teacher code exists and if this is the same to update
                var existingTeacherWithCode = await _teachersRepository.GetByCodeAsync(command.Code);
                if (existingTeacherWithCode != null)
                {
                    if (existingTeacher.Id != existingTeacherWithCode.Id)
                    {
                        var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.UpdateTeacherContent00002);
                        var errorMessageArgs = new string[] { command.Code };
                        var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                        throw new ContentValidationException(
                                    handledError.PropertyName,
                                    handledError.ErrorCode,
                                    errorMessage,
                                    HttpStatusCode.Conflict);
                    }
                }
                existingTeacher.Code = command.Code;
            }
            if (!string.IsNullOrEmpty(command.Firstname))
            {
                existingTeacher.Firstname = command.Firstname;
            }
            if (!string.IsNullOrEmpty(command.Lastname))
            {
                existingTeacher.Lastname = command.Lastname;
            }

            // Save teacher information
            var newTeacher = await _teachersRepository.UpdateAsync(existingTeacher);

            // Map newData to response
            var response = _mapper.Map<UpdateTeacherVm>(newTeacher);

            //
            return response;
        }
    }
}
