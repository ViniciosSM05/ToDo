using DDDApi.Domain.Core.Interfaces.Queue;
using Microsoft.Extensions.Configuration;

namespace DDDApi.Infra.Queue.Configurations
{
    public class EmailQueueConfiguration : IEmailQueueConfiguration
    {
        private const string emailQueueSectionName = "EmailQueueSettings";
        public EmailQueueConfiguration(IConfiguration configuration)
        {
            var section = configuration.GetSection(emailQueueSectionName);
            Host = section.GetValue<string>(nameof(Host));
            Name = section.GetValue<string>(nameof(Name));
        }

        public string Host { get; private set; }
        public string Name { get; private set; }
    }
}
