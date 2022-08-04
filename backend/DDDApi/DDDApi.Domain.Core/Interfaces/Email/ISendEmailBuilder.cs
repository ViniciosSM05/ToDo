using DDDApi.Domain.Core.DTO.Email;

namespace DDDApi.Domain.Core.Interfaces.Email
{
    public interface ISendEmailBuilder
    {
        ISendEmailBuilder WithSubject(string subject);
        ISendEmailBuilder WithBodyText(string bodyText);
        ISendEmailBuilder WithBodyHTML(string bodyHTML);
        ISendEmailBuilder WithRecipient(string recipient);
        SendEmailDTO Build();
        void Clear();
    }
}
