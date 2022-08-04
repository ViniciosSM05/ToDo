using DDDApi.Domain.Core.DTO.Email;
using DDDApi.Domain.Core.Interfaces.Email;

namespace DDDApi.Infra.Email.Builders
{
    public class SendEmailBuilder : ISendEmailBuilder
    {
        private string Subject { get; set; }
        private string BodyText { get; set; }
        private string BodyHTML { get; set; }
        private string Recipient { get; set; }

        public SendEmailDTO Build() => new (Subject, BodyText, BodyHTML, Recipient);

        public void Clear()
        {
            Subject = null;
            BodyText = null;
            BodyHTML = null;
            Recipient = null;
        }

        public ISendEmailBuilder WithBodyHTML(string bodyHTML)
        {
            BodyHTML = bodyHTML;
            return this;
        }

        public ISendEmailBuilder WithBodyText(string bodyText)
        {
            BodyText = bodyText;
            return this;
        }

        public ISendEmailBuilder WithRecipient(string recipient)
        {
            Recipient = recipient;
            return this;
        }

        public ISendEmailBuilder WithSubject(string subject)
        {
            Subject = subject;
            return this;
        }
    }
}
