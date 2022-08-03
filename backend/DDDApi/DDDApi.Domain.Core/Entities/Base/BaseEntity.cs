namespace DDDApi.Domain.Core.Entities.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime InData { get; set; }
        public DateTime? MoData { get; set; }
    }
}
