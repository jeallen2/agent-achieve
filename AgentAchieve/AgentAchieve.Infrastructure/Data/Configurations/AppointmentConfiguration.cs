using AgentAchieve.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgentAchieve.Infrastructure.Data.Configurations;

public class AppointmentConfiguration : BaseAuditableEntityConfiguration<Appointment>
{
    public override void Configure(EntityTypeBuilder<Appointment> builder)
    {
        base.Configure(builder); // Call the base configuration 

        // Other client-specific configurations can be added here
    }
}