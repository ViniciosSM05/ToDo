using DDDApi.Domain.Core.DTO.Messages;
using DDDApi.Domain.Core.Interfaces.Notification;
using FluentValidation.Results;

namespace DDDApi.Domain.Notifications
{
    public class Notification : INotification
    {
        public IReadOnlyCollection<string> Messages { get; private set; } = new List<string>();
        public IReadOnlyCollection<FieldMessagesDTO> FieldMessages { get; private set; } = new List<FieldMessagesDTO>();
        public bool IsValid => !Messages.Any() && !FieldMessages.Any();

        public void AddFieldMessages(ValidationResult messages)
        {
            messages.Errors.ForEach(error =>
            {
                var fieldMessage = FieldMessages.FirstOrDefault(x => x.FieldName == error.PropertyName);
                if (fieldMessage != null)
                    fieldMessage.AddMessage(error.ErrorMessage);
                else
                    FieldMessages = FieldMessages.Append(new FieldMessagesDTO(error.PropertyName, error.ErrorMessage)).ToList();
            });
        }

        public void AddMessage(string message) => Messages = Messages.Append(message).ToList();

    }
}
