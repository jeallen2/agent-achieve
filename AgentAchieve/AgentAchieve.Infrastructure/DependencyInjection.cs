using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Data;
using AgentAchieve.Infrastructure.Data.Interceptors;
using AgentAchieve.Infrastructure.Features.Appointments;
using AgentAchieve.Infrastructure.Features.Clients;
using AgentAchieve.Infrastructure.Features.Identity;
using AgentAchieve.Infrastructure.Features.Properties;
using AgentAchieve.Infrastructure.Features.Sales;
using AgentAchieve.Infrastructure.Features.SalesGoals;
using AgentAchieve.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AgentAchieve.Infrastructure;

/// <summary>
/// Provides extension methods for configuring dependency injection in the application.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configures the infrastructure services for dependency injection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing the application settings.</param>
    /// <param name="databaseName">The name of the database to use (optional).</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, string? databaseName = null)
    {
        // Database setup
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseInMemoryDatabase(databaseName ?? "AgentAchieveInMemoryDb");
                options.EnableSensitiveDataLogging();
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });
        }
        else
        {
            var connectionName = "DefaultConnection";
            var connectionString = configuration.GetConnectionString(connectionName) ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });
        }

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IRepository<Client>, Repository<Client>>();

        services.AddScoped<IIdentityService, IdentityService>()
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddScoped<IClientService, ClientService>()
            .AddScoped<IPropertyService, PropertyService>()
            .AddScoped<ISaleService, SaleService>()
            .AddScoped<ISalesGoalService, SalesGoalService>()
            .AddScoped<IAppointmentService, AppointmentService>()
            .AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        return services;
    }
}
