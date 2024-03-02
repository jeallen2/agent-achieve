using AgentAchieve.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgentAchieve.Infrastructure.Data.Configurations;

public class ClientConfiguration : BaseAuditableEntityConfiguration<Client>
{
    public override void Configure(EntityTypeBuilder<Client> builder)
    {
        base.Configure(builder); // Call the base configuration first

        // Other client-specific configurations can be added here
    }
}
