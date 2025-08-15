using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentHandler :
        IRequestHandler<CreateEnrollmentCommand, CreateEnrollmentVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<CreateEnrollmentHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IEnrollmentsRepository _enrollmentsRepository;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IStudentsRepository _studentsRepository;

        public CreateEnrollmentHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<CreateEnrollmentHandler> logger,
            IMapper mapper,
            IEnrollmentsRepository enrollmentsRepository,
            ICoursesRepository coursesRepository,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _enrollmentsRepository = enrollmentsRepository;
            _coursesRepository = coursesRepository;
            _studentsRepository = studentsRepository;
        }

        public async Task<CreateEnrollmentVm> Handle(CreateEnrollmentCommand command, CancellationToken cancellationToken)
        {
            // Verify if enrollment exists
            var existingEnrollment = await _enrollmentsRepository.GetEnrollmentsByStudentIdAsync(command.CourseId.Value, command.StudentId.Value);
            if (existingEnrollment != null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.CreateEnrollmentContent00001);
                var errorMessageArgs = new string[] { command.CourseId.Value.ToString(),
                                                        command.StudentId.Value.ToString(),
                                                        existingEnrollment.Id.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.Conflict);
            }

            // Verify if course exists
            var existingCourse = await _coursesRepository.GetByIdAsync(command.CourseId.Value);
            if (existingCourse == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.CreateEnrollmentContent00002);
                var errorMessageArgs = new string[] { command.CourseId.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.Conflict);
            }

            // Verify if student exists
            var existingStudent = await _studentsRepository.GetByIdAsync(command.StudentId.Value);
            if (existingStudent == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.CreateEnrollmentContent00003);
                var errorMessageArgs = new string[] { command.StudentId.Value.ToString() };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.Conflict);
            }

            // Save teacher information
            var newEnrollment = await _enrollmentsRepository.CreateAsync(
                new Enrollment
                {
                    CourseId = command.CourseId,
                    StudentId = command.StudentId
                }
            );

            // Map newData to response
            var response = _mapper.Map<CreateEnrollmentVm>(newEnrollment);

            //
            return response;
        }
    }
}
