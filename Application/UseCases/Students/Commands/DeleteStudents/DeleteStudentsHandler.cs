using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Students.Commands.DeleteStudents
{
    public class DeleteStudentsHandler :
        IRequestHandler<DeleteStudentsCommand, DeleteStudentsVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<DeleteStudentsHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _studentsRepository;

        public DeleteStudentsHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<DeleteStudentsHandler> logger,
            IMapper mapper,
            IStudentsRepository studentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _studentsRepository = studentsRepository;
        }

        public async Task<DeleteStudentsVm> Handle(DeleteStudentsCommand command, CancellationToken cancellationToken)
        {
            // Delete rows
            var affectedRows = await _studentsRepository.DeleteAsync(command.Ids);

            // Map rows affected
            if (affectedRows > 0)
            {
                return new DeleteStudentsVm
                {
                    WereDeleted = true,
                    TotalAffected = affectedRows
                };
            }

            //
            return new DeleteStudentsVm
            {
                WereDeleted = false,
                TotalAffected = 0
            };
        }
    }
}
