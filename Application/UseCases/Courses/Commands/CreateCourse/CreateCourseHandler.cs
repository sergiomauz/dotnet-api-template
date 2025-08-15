using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Courses.Commands.CreateCourse
{
    public class CreateCourseHandler :
        IRequestHandler<CreateCourseCommand, CreateCourseVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<CreateCourseHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;
        private readonly ITeachersRepository _teachersRepository;

        public CreateCourseHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<CreateCourseHandler> logger,
            IMapper mapper,
            ICoursesRepository coursesRepository,
            ITeachersRepository teachersRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _coursesRepository = coursesRepository;
            _teachersRepository = teachersRepository;
        }

        public async Task<CreateCourseVm> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            // Verify if course exists
            var existingCourse = await _coursesRepository.GetByCodeAsync(command.Code);
            if (existingCourse != null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.CreateCourseContent00001);
                var errorMessageArgs = new string[] { command.Code };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.Conflict);
            }

            // Verify if teacher exists
            var existingTeacher = await _teachersRepository.GetByIdAsync(command.TeacherId.Value);
            if (existingTeacher == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.CreateCourseContent00002);
                var errorMessage = handledError.ErrorMessage;
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.Conflict);
            }

            // Save course information
            var newCourse = await _coursesRepository.CreateAsync(
                new Course
                {
                    TeacherId = command.TeacherId,
                    Code = command.Code,
                    Name = command.Name,
                    Description = command.Description
                }
            );

            // Map newData to response
            var response = _mapper.Map<CreateCourseVm>(newCourse);

            //
            return response;
        }
    }
}
