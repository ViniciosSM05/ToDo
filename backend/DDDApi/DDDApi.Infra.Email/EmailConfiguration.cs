using DDDApi.Domain.Core.Interfaces.Email;
using Microsoft.Extensions.Configuration;

namespace DDDApi.Infra.Email
{
    public class EmailConfiguration : IEmailConfiguration
    {
        private const string emailConfigSectionName = "EmailSettings";
        public EmailConfiguration(IConfiguration configuration)
        {
            var section = configuration.GetSection(emailConfigSectionName);
            Name = section.GetValue<string>(nameof(Name));
            Address = section.GetValue<string>(nameof(Address));
            Password = section.GetValue<string>(nameof(Password));
            SMTP = section.GetValue<string>(nameof(SMTP));
            Port = section.GetValue<int>(nameof(Port));
            Ssl = section.GetValue<bool>(nameof(Ssl));
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Password { get; private set; }
        public string SMTP { get; private set; }
        public int Port { get; private set ; }
        public bool Ssl { get; private set ; }
    }
}
