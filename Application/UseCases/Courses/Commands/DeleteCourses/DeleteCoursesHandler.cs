using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Courses.Commands.DeleteCourses
{
    public class DeleteCoursesHandler :
        IRequestHandler<DeleteCoursesCommand, DeleteCoursesVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<DeleteCoursesHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;

        public DeleteCoursesHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<DeleteCoursesHandler> logger,
            IMapper mapper,
            ICoursesRepository coursesRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _coursesRepository = coursesRepository;
        }

        public async Task<DeleteCoursesVm> Handle(DeleteCoursesCommand command, CancellationToken cancellationToken)
        {
            // Delete rows
            var affectedRows = await _coursesRepository.DeleteAsync(command.Ids);

            // Map rows affected
            if (affectedRows > 0)
            {
                return new DeleteCoursesVm
                {
                    WereDeleted = true,
                    TotalAffected = affectedRows
                };
            }

            //
            return new DeleteCoursesVm
            {
                WereDeleted = false,
                TotalAffected = 0
            };
        }
    }
}
