using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Courses.Commands.UpdateCourse
{
    public class UpdateCourseHandler :
        IRequestHandler<UpdateCourseCommand, UpdateCourseVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<UpdateCourseHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;
        private readonly ITeachersRepository _teachersRepository;

        public UpdateCourseHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<UpdateCourseHandler> logger,
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

        public async Task<UpdateCourseVm> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
        {
            // Verify if course exists
            var existingCourse = await _coursesRepository.GetByIdAsync(command.Id.Value);
            if (existingCourse == null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.UpdateCourseContent00001);
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
                // Verify if a valid course code exists and if this is the same to update
                var existingCourseWithCode = await _coursesRepository.GetByCodeAsync(command.Code);
                if (existingCourseWithCode != null)
                {
                    if (existingCourse.Id != existingCourseWithCode.Id)
                    {
                        var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.UpdateCourseContent00002);
                        var errorMessageArgs = new string[] { command.Code };
                        var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                        throw new ContentValidationException(
                                    handledError.PropertyName,
                                    handledError.ErrorCode,
                                    errorMessage,
                                    HttpStatusCode.Conflict);
                    }
                }
                existingCourse.Code = command.Code;
            }
            if (command.TeacherId.HasValue)
            {
                // Verify if teacher exists
                var existingTeacher = await _teachersRepository.GetByIdAsync(command.TeacherId.Value);
                if (existingTeacher == null)
                {
                    var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.UpdateCourseContent00003);
                    var errorMessageArgs = new string[] { command.TeacherId.Value.ToString() };
                    var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                    throw new ContentValidationException(
                                handledError.PropertyName,
                                handledError.ErrorCode,
                                errorMessage,
                                HttpStatusCode.Conflict);
                }
                existingCourse.TeacherId = Convert.ToInt32(command.TeacherId);
            }
            if (!string.IsNullOrEmpty(command.Name))
            {
                existingCourse.Name = command.Name;
            }
            if (!string.IsNullOrEmpty(command.Name))
            {
                existingCourse.Name = command.Name;
            }

            // Save course information
            var newCourse = await _coursesRepository.UpdateAsync(existingCourse);

            // Map newData to response
            var response = _mapper.Map<UpdateCourseVm>(newCourse);

            //
            return response;
        }
    }
}
