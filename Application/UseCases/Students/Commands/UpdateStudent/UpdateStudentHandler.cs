using System.Net;
using System.Globalization;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Students.Commands.UpdateStudent
{
    public class UpdateStudentHandler :
        IRequestHandler<UpdateStudentCommand, UpdateStudentVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<UpdateStudentHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _studentsRepository;

        public UpdateStudentHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<UpdateStudentHandler> logger,
            IMapper mapper,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _studentsRepository = studentsRepository;
        }

        public async Task<UpdateStudentVm> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            // Verify if student exists
            var existingStudent = await _studentsRepository.GetByIdAsync(command.Id.Value);
            if (existingStudent == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentContent00001);
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
                // Verify if a valid student code exists and if this is the same to update
                var existingStudentWithCode = await _studentsRepository.GetByCodeAsync(command.Code);
                if (existingStudentWithCode != null)
                {
                    if (existingStudent.Id != existingStudentWithCode.Id)
                    {
                        var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.UpdateStudentContent00002);
                        var errorMessageArgs = new string[] { command.Code };
                        var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                        throw new ContentValidationException(
                                    handledError.PropertyName,
                                    handledError.ErrorCode,
                                    errorMessage,
                                    HttpStatusCode.Conflict);
                    }
                }
                existingStudent.Code = command.Code;
            }
            if (!string.IsNullOrEmpty(command.Firstname))
            {
                existingStudent.Firstname = command.Firstname;
            }
            if (!string.IsNullOrEmpty(command.Lastname))
            {
                existingStudent.Lastname = command.Lastname;
            }
            if (!string.IsNullOrEmpty(command.BirthDate))
            {
                existingStudent.BirthDate = DateTime.ParseExact(command.BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            // Save student information
            var newStudent = await _studentsRepository.UpdateAsync(existingStudent);

            // Map newData to response
            var response = _mapper.Map<UpdateStudentVm>(newStudent);

            //
            return response;
        }
    }
}
