namespace DDDApi.Domain.Core.DTO.Messages
{
    public class FieldMessagesDTO
    {
        public FieldMessagesDTO(string fieldName, string message)
        {
            FieldName = fieldName;
            Messages = new List<string>{ message };
        }

        public string FieldName { get; private set; }
        public IReadOnlyCollection<string> Messages { get; private set; }
        public void AddMessage(string message) => Messages = Messages.Append(message).ToList();
    }
}
