using AgentAchieve.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AgentAchieve.Infrastructure.Data;

/// <summary>
/// Represents the application's database context.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
/// </remarks>
/// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    /// <summary>
    /// Gets or sets the clients in the database.
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Gets or sets the properties in the database.
    /// </summary>
    public DbSet<Property> Properties { get; set; }

    /// <summary>
    /// Gets or sets the sales in the database.
    /// </summary>
    public DbSet<Sale> Sales { get; set; }

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types
    /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
    /// </summary>
    /// <param name="builder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Configures the database context options.
    /// </summary>
    /// <param name="optionsBuilder">A builder used to create or modify options for this context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Register interceptors dynamically
        var serviceProvider = optionsBuilder.Options.FindExtension<CoreOptionsExtension>()?.ApplicationServiceProvider;
        if (serviceProvider != null)
        {
            optionsBuilder.AddInterceptors(ServiceProviderServiceExtensions.GetServices<ISaveChangesInterceptor>(serviceProvider));
        }
    }
}
