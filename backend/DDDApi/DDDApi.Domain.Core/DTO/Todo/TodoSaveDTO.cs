namespace DDDApi.Domain.Core.DTO.Todo
{
    public class TodoSaveDTO
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }
}
