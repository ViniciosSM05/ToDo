using DDDApi.Domain.Core.DTO.Messages;
using DDDApi.Domain.Core.Interfaces.Notification;

namespace DDDApi.WebApplication.ViewModels
{
    public class ResponseViewModel<T>
    {
        public ResponseViewModel(INotification notification, T data)
        {
            Success = notification.IsValid;
            Messages = notification.Messages.ToList();
            FieldMessages = notification.FieldMessages.ToList();
            Data = data;
        }

        public ResponseViewModel(INotification notification)
        {
            Success = notification.IsValid;
            Messages = notification.Messages.ToList();
            FieldMessages = notification.FieldMessages.ToList();

        }

        public ResponseViewModel(Exception ex)
        {
            Success = false;
            Error = ex.Message;
        }

        public bool Success { get; set; }
        public IList<string> Messages { get; set; }
        public IList<FieldMessagesDTO> FieldMessages { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }
}
