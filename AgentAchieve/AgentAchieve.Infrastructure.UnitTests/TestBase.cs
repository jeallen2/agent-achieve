using AgentAchieve.Core.Common;
using AgentAchieve.Infrastructure.Data;
using AgentAchieve.Infrastructure.Features.Clients;
using AgentAchieve.Infrastructure.Features.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AgentAchieve.Infrastructure.UnitTests;

/// <summary>
/// Base class for unit tests.
/// </summary>
/// <typeparam name="TClass">The type of the class being tested.</typeparam>
public class TestBase<TClass> : IClassFixture<DatabaseFixture>, IDisposable
{
    /// <summary>
    /// AutoMocker for setting up mocks.
    /// </summary>
    protected readonly AutoMocker AutoMocker;

    /// <summary>
    /// Logger for logging messages.
    /// </summary>
    protected readonly ILogger<TClass> Logger;

    /// <summary>
    /// Factory for creating service scopes.
    /// </summary>
    protected IServiceScopeFactory _scopeFactory;

    /// <summary>
    /// Fixture for setting up the database.
    /// </summary>
    protected readonly DatabaseFixture DbFixture;

    /// <summary>
    /// The application database context.
    /// </summary>
    protected ApplicationDbContext? Context => DbFixture.Context;

    /// <summary>
    /// Configuration for the application.
    /// </summary>
    protected readonly IConfiguration Configuration;

    /// <summary>
    /// The name of the in-memory database so consistent per test class.
    /// </summary>
    private static readonly string InMemoryDbName = Guid.NewGuid().ToString();

    /// <summary>
    /// Initializes a new instance of the <see cref="TestBase{TClass}"/> class.
    /// </summary>
    /// <param name="outputHelper">Helper for writing to the test output.</param>
    /// <param name="dbFixture">Fixture for setting up the database.</param>
    public TestBase(ITestOutputHelper outputHelper, DatabaseFixture dbFixture)
    {
        DbFixture = dbFixture;

        AutoMocker = new AutoMocker();
        Logger = outputHelper.ToLogger<TClass>();
        AutoMocker.Use(Logger);

        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        _scopeFactory = CreateServiceScopeFactory();
    }

    /// <summary>
    /// Creates a service scope factory.
    /// </summary>
    /// <returns>The created service scope factory.</returns>
    protected IServiceScopeFactory CreateServiceScopeFactory()
    {
        var services = new ServiceCollection();

        // Add your infrastructure with configuration
        services.AddInfrastructure(Configuration, InMemoryDbName);

        // Retrieve DbContext and pass to DatabaseFixture
        var dbContext = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();

        // Ensure the database is created
        dbContext.Database.EnsureCreated();

        // Remove existing registration for ICurrentUserService
        var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(ICurrentUserService));
        services.Remove(currentUserServiceDescriptor!);

        // Mock ICurrentUserService 
        var mockCurrentUserService = new Mock<ICurrentUserService>();
        mockCurrentUserService.Setup(s => s.GetUserIdAsync()).ReturnsAsync("test-user-id");
        mockCurrentUserService.Setup(s => s.GetUserNameAsync()).ReturnsAsync("Test User");
        // Add the mocked service to the DI container
        services.AddScoped(provider => mockCurrentUserService.Object);

        DbFixture.Context = dbContext;

        var serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true // Helps identify potential DI issues
        });

        var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>() ?? throw new Exception("Failed to retrieve IServiceScopeFactory from the service provider.");
        return serviceScopeFactory;
    }

    /// <summary>
    /// Finds an entity by its ID.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>The found entity, or null if no entity was found.</returns>
    protected async Task<TEntity?> FindAsync<TEntity>(object id) where TEntity : class, IEntity
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await context.FindAsync<TEntity>(id);
    }

    /// <summary>
    /// Adds an entity to the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected async Task AddAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Add(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Logs the description of the test.
    /// </summary>
    /// <param name="callerName">The name of the calling method.</param>
    protected void LogDescription([CallerMemberName] string callerName = "")
    {
        var callingMethod = GetType().GetMethod(callerName);
        var traits = TraitHelper.GetTraits(callingMethod);
        var descriptionTrait = traits.FirstOrDefault(t => t.Key == "Description"); // Find the 'Description' trait

        if (descriptionTrait.Value != null) // Check if the trait's Value is null
        {
            Logger.LogInformation("Test Description: {Description}", descriptionTrait.Value);
        }
    }

    /// <summary>
    /// Creates a client service.
    /// </summary>
    /// <returns>The created client service.</returns>
    public IClientService CreateClientService()
    {
        var scope = _scopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<IClientService>();
    }

    /// <summary>
    /// Performs the necessary cleanup operations when the object is being disposed.
    /// </summary>
    public void Dispose()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted();
    }
}