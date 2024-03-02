using AgentAchieve.Core.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace AgentAchieve.Infrastructure.Data.Configurations;

public abstract class BaseAuditableEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseAuditableEntity<int>
{
    public virtual void Configure(EntityTypeBuilder<T> builder) 
    {
        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();
    }
}