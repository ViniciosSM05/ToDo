using DDDApi.Domain.Core.Entities.Base;

namespace DDDApi.Domain.Core.Entities
{
    public class Todo : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
