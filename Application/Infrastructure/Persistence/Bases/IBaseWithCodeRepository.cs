using Domain.Entities.Bases;


namespace Application.Infrastructure.Persistence.Bases
{
    public interface IBaseWithCodeRepository<T> : IBaseWithIdRepository<T>
        where T : BaseEntityWithCode
    {
        Task<T?> GetByCodeAsync(string code);
    }
}
