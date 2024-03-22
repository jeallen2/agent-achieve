using AgentAchieve.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgentAchieve.Infrastructure.Data.Configurations;

public class SalesGoalConfiguration : BaseAuditableEntityConfiguration<SalesGoal>
{
    public override void Configure(EntityTypeBuilder<SalesGoal> builder)
    {
        base.Configure(builder); // Call the base configuration first

        builder.HasOne(x => x.OwnedBy)
               .WithMany(x => x.SalesGoals)
               .HasForeignKey(x => x.OwnedById)
               .OnDelete(DeleteBehavior.Restrict);

        // Unique Constraint
        builder.HasIndex(x => new { x.OwnedById, x.GoalDate })
               .IsUnique();
    }
}
