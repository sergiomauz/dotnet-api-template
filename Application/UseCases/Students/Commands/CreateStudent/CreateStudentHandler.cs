using System.Net;
using System.Globalization;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Students.Commands.CreateStudent
{
    public class CreateStudentHandler :
        IRequestHandler<CreateStudentCommand, CreateStudentVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<CreateStudentHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _studentsRepository;

        public CreateStudentHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<CreateStudentHandler> logger,
            IMapper mapper,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _studentsRepository = studentsRepository;
        }

        public async Task<CreateStudentVm> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            // Verify if student exists
            var existingStudent = await _studentsRepository.GetByCodeAsync(command.Code);
            if (existingStudent != null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.CreateStudentContent00001);
                var errorMessageArgs = new string[] { command.Code };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.Conflict);
            }

            // Save student information
            var newTeacher = await _studentsRepository.CreateAsync(
                new Student
                {
                    Code = command.Code,
                    Firstname = command.Firstname,
                    Lastname = command.Lastname,
                    BirthDate = DateTime.ParseExact(command.BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                }
            );

            // Map newData to response
            var response = _mapper.Map<CreateStudentVm>(newTeacher);

            //
            return response;
        }
    }
}
