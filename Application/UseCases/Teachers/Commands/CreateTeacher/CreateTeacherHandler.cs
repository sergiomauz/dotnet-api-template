using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Application.Commons.Exceptions;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherHandler :
        IRequestHandler<CreateTeacherCommand, CreateTeacherVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<CreateTeacherHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITeachersRepository _teachersRepository;

        public CreateTeacherHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<CreateTeacherHandler> logger,
            IMapper mapper,
            ITeachersRepository teachersRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _teachersRepository = teachersRepository;
        }

        public async Task<CreateTeacherVm> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
        {
            // Verify if teacher exists
            var existingTeacher = await _teachersRepository.GetByCodeAsync(command.Code);
            if (existingTeacher != null)
            {
                var handledError = _errorCatalogService.GetErrorByCode(ErrorConstants.CreateTeacherContent00001);
                var errorMessageArgs = new string[] { command.Code };
                var errorMessage = string.Format(handledError.ErrorMessage, errorMessageArgs);
                throw new ContentValidationException(
                            handledError.PropertyName,
                            handledError.ErrorCode,
                            errorMessage,
                            HttpStatusCode.Conflict);
            }

            // Save teacher information
            var newTeacher = await _teachersRepository.CreateAsync(
                new Teacher
                {
                    Code = command.Code,
                    Firstname = command.Firstname,
                    Lastname = command.Lastname
                }
            );

            // Map newData to response
            var response = _mapper.Map<CreateTeacherVm>(newTeacher);

            //
            return response;
        }
    }
}
