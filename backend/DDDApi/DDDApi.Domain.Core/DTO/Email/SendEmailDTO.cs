namespace DDDApi.Domain.Core.DTO.Email
{
    public class SendEmailDTO
    {
        public SendEmailDTO(string subject, string bodyText, string bodyHTML, string recipient)
        {
            Subject = subject;
            BodyText = bodyText;
            BodyHTML = bodyHTML;
            Recipient = recipient;
        }

        public string Subject { get; private set; }
        public string BodyText { get; private set; }
        public string BodyHTML { get; private set; }
        public string Recipient { get; private set; }
    }
}
