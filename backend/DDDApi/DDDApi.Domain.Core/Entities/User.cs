using DDDApi.Domain.Core.Entities.Base;

namespace DDDApi.Domain.Core.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Todos = new HashSet<Todo>();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}
