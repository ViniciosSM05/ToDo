namespace DDDApi.Domain.Core.DTO.Todo
{
    public class TodoCheckDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
