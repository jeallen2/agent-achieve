namespace AgentAchieve.Infrastructure.Data.Configurations;

//public class OwnerPropertyEntityConfiguration<T> : BaseAuditableEntityConfiguration<T> where T : OwnerPropertyEntity
//{
//    public override void Configure(EntityTypeBuilder<T> builder)
//    {
//        base.Configure(builder);

//        // ... Common configurations specific to OwnerPropertyEntity types
//        builder.HasOne(x => x.Owner)
//               .WithMany()
//               .HasForeignKey(x => x.CreatedBy)
//               .OnDelete(DeleteBehavior.Restrict);

//        builder.HasOne(x => x.Editor)
//               .WithMany()
//               .HasForeignKey(x => x.LastModifiedBy)
//               .OnDelete(DeleteBehavior.Restrict);

//    }
//}
