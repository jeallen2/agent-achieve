using AgentAchieve.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgentAchieve.Infrastructure.Data.Configurations;

public class SaleConfiguration : BaseAuditableEntityConfiguration<Sale>
{
    public override void Configure(EntityTypeBuilder<Sale> builder)
    {
        base.Configure(builder); // Call the base configuration first

        builder
            .HasOne(s => s.Client)
            .WithMany(/* Should Sales be added to Client.Sales? */)
            .HasForeignKey(s => s.ClientId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder
            .HasOne(s => s.Property)
            .WithMany(/* Should Sales be added to Property.Sales */)
            .HasForeignKey(s => s.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.OwnedBy)
               .WithMany(x => x.Sales)
               .HasForeignKey(x => x.OwnedById)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
