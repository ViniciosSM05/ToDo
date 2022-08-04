namespace DDDApi.Domain.Core.Interfaces.Email
{
    public interface IEmailConfiguration
    {
        string Name { get; }
        string Address { get; }
        string Password { get; }
        string SMTP { get; }
        int Port { get; }
        bool Ssl { get; }
    }
}
