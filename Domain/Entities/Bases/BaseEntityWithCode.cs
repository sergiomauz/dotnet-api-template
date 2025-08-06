namespace Domain.Entities.Bases
{
    public abstract class BaseEntityWithCode : BaseEntityWithId
    {
        public string? Code { get; set; }
    }
}
