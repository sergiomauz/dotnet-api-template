using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Teachers.Commands.DeleteTeachers
{
    public class DeleteTeachersHandler :
        IRequestHandler<DeleteTeachersCommand, DeleteTeachersVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<DeleteTeachersHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITeachersRepository _teachersRepository;

        public DeleteTeachersHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<DeleteTeachersHandler> logger,
            IMapper mapper,
            ITeachersRepository teachersRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _teachersRepository = teachersRepository;
        }

        public async Task<DeleteTeachersVm> Handle(DeleteTeachersCommand command, CancellationToken cancellationToken)
        {
            // Delete rows
            var affectedRows = await _teachersRepository.DeleteAsync(command.Ids);

            // Map rows affected
            if (affectedRows > 0)
            {
                return new DeleteTeachersVm
                {
                    WereDeleted = true,
                    TotalAffected = affectedRows
                };
            }

            //
            return new DeleteTeachersVm
            {
                WereDeleted = false,
                TotalAffected = 0
            };
        }
    }
}
