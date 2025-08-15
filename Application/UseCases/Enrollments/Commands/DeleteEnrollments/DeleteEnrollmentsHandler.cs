using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Application.ErrorCatalog;
using Application.Infrastructure.Persistence;


namespace Application.UseCases.Enrollments.Commands.DeleteEnrollments
{
    public class DeleteEnrollmentsHandler :
        IRequestHandler<DeleteEnrollmentsCommand, DeleteEnrollmentsVm>
    {
        private readonly IErrorCatalogService _errorCatalogService;
        private readonly ILogger<DeleteEnrollmentsHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IEnrollmentsRepository _enrollmentsRepository;

        public DeleteEnrollmentsHandler(
            IErrorCatalogService errorCatalogService,
            ILogger<DeleteEnrollmentsHandler> logger,
            IMapper mapper,
            IEnrollmentsRepository enrollmentsRepository)
        {
            _errorCatalogService = errorCatalogService;
            _logger = logger;
            _mapper = mapper;
            _enrollmentsRepository = enrollmentsRepository;
        }

        public async Task<DeleteEnrollmentsVm> Handle(DeleteEnrollmentsCommand command, CancellationToken cancellationToken)
        {
            // Delete rows
            var affectedRows = await _enrollmentsRepository.DeleteAsync(command.Ids.Select(x => Guid.Parse(x)));

            // Map rows affected
            if (affectedRows > 0)
            {
                return new DeleteEnrollmentsVm
                {
                    WereDeleted = true,
                    TotalAffected = affectedRows
                };
            }

            //
            return new DeleteEnrollmentsVm
            {
                WereDeleted = false,
                TotalAffected = 0
            };
        }
    }
}
