using AgentAchieve.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgentAchieve.Infrastructure.Data.Configurations;

public class PropertyConfiguration : BaseAuditableEntityConfiguration<Property>
{
    public override void Configure(EntityTypeBuilder<Property> builder)
    {
        base.Configure(builder); // Call the base configuration first

    }
}
