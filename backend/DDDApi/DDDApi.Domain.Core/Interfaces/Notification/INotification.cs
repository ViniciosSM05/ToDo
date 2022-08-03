using DDDApi.Domain.Core.DTO.Messages;
using FluentValidation.Results;

namespace DDDApi.Domain.Core.Interfaces.Notification
{
    public interface INotification
    {
        IReadOnlyCollection<string> Messages { get; }
        IReadOnlyCollection<FieldMessagesDTO> FieldMessages { get; }
        bool IsValid { get; }
        void AddMessage(string message);
        void AddFieldMessages(ValidationResult messages);
    }
}
