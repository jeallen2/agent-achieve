using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Clients;
using AgentAchieve.Infrastructure.Features.Identity;
using AgentAchieve.Infrastructure.Features.Properties;
using AgentAchieve.Infrastructure.Features.Sales;
using AgentAchieve.Infrastructure.Features.SalesGoals;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq.AutoMock;
using System.Runtime.CompilerServices;
using Telerik.Blazor.Components;
using Telerik.Blazor.Services;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AgentAchieve.WebUi.Server.UnitTests.Common;
public class TelerikTestContext : TestContext
{
    private IRenderedComponent<TelerikRootComponent>? rootComponent;
    public IRenderedComponent<TelerikRootComponent> RootComponent
        => rootComponent ?? throw new InvalidOperationException("The RootComponent is not available before a component has been rendered with the TestContext.");
    protected ILogger<TelerikTestContext> Logger;
    public TelerikTestContext(ITestOutputHelper outputHelper)
    {
        Logger = outputHelper.ToLogger<TelerikTestContext>();
        JSInterop.SetupVoid(x => x.Identifier.StartsWith("TelerikBlazor", StringComparison.InvariantCultureIgnoreCase));
        JSInterop.Setup<string>(x => x.Identifier.StartsWith("TelerikBlazor", StringComparison.InvariantCultureIgnoreCase)).SetResult(string.Empty);
        JSInterop.Setup<bool>(x => x.Identifier.StartsWith("TelerikBlazor", StringComparison.InvariantCultureIgnoreCase)).SetResult(default);
        JSInterop.Setup<double>(x => x.Identifier.StartsWith("TelerikBlazor", StringComparison.InvariantCultureIgnoreCase)).SetResult(default);

        // add the Telerik Blazor services like in a regular app
        Services.AddTelerikBlazor();
        Services.AddSingleton<ITelerikStringLocalizer, DefaultTelerikStringLocalizer>();

        // Create AutoMocker and set up mocks
        var mocker = new AutoMocker();

        // Mock data for SaleDto
        var mockSales = new List<SaleDto>
        {
            new SaleDto
            {
                Id = 1,
                OwnedById = "1",
                OwnerFullName = "First User",
                PropertyId = 1,
                PropertyFullAddress = "123 First St",
                ClientId = 1,
                ClientFullNameAndPhone = "Client1, 123-456-7890",
                SaleType = SaleType.Buyer, // Assuming SaleType.Buyer is a valid value
                ClosingDate = new DateTime(2022, 1, 1),
                SalePrice = 500,
                CommissionRate = 5
            },
            new SaleDto
            {
                Id = 2,
                OwnedById = "2",
                OwnerFullName = "Second User",
                PropertyId = 2,
                PropertyFullAddress = "456 Second St",
                ClientId = 2,
                ClientFullNameAndPhone = "Client2, 987-654-3210",
                SaleType = SaleType.Seller, // Assuming SaleType.Seller is a valid value
                ClosingDate = new DateTime(2022, 2, 1),
                SalePrice = 1000,
                CommissionRate = 10
            }
        };

        


        // Mock ISaleService
        var saleServiceMock = mocker.GetMock<ISaleService>();
        saleServiceMock.Setup(service => service.GetAllDtoAsync())
            .Returns(Task.FromResult(mockSales.AsEnumerable()));
        Services.AddSingleton(saleServiceMock.Object);

        // Mock data for SalesGoalDto
        var mockSalesGoals = new List<SalesGoalDto>
        {
            new SalesGoalDto
            {
                Id = 1,
                OwnedById = "1",
                OwnerFullName = "First User",
                GoalMonthYear = new DateTime(2022, 1, 1),
                SalesGoalAmount = 1000,
                Sales = mockSales.Where(s => s.OwnedById == "1").ToList()
            },
            new SalesGoalDto
            {
                Id = 2,
                OwnedById = "2",
                OwnerFullName = "Second User",
                GoalMonthYear = new DateTime(2022, 2, 1),
                SalesGoalAmount = 2000,
                Sales = mockSales.Where(s => s.OwnedById == "2").ToList()
            }
        };

        // Mock ISaleGoalsService
        var salesGoalServiceMock = mocker.GetMock<ISalesGoalService>();
        salesGoalServiceMock.Setup(service => service.GetAllDtoAsync())
            .Returns(Task.FromResult(mockSalesGoals.AsEnumerable()));
        Services.AddSingleton(salesGoalServiceMock.Object);

        // Mock IClientService
        var clientServiceMock = mocker.GetMock<IClientService>();
        clientServiceMock.Setup(service => service.GetAllDtoAsync())
            .Returns(Task.FromResult(Enumerable.Empty<ClientDto>()));
        Services.AddSingleton(clientServiceMock.Object);

        // Mock IPropertyService
        var propertyServiceMock = mocker.GetMock<IPropertyService>();
        propertyServiceMock.Setup(service => service.GetAllDtoAsync())
            .Returns(Task.FromResult(Enumerable.Empty<PropertyDto>()));
        Services.AddSingleton(propertyServiceMock.Object);

        // Mock data for ApplicationUserDto
        var mockUsers = new List<ApplicationUserDto>
        {
            new ApplicationUserDto
            {
                Id = "1",
                UserName = "User1",
                FirstName = "First",
                LastName = "User"
            },
            new ApplicationUserDto
            {
                Id = "2",
                UserName = "User2",
                FirstName = "Second",
                LastName = "User"
            }
        };

        // Mock IIdentityService
        var identityServiceMock = mocker.GetMock<IIdentityService>();
        identityServiceMock.Setup(service => service.GetUsers())
            .Returns(Task.FromResult(mockUsers.AsEnumerable()));
        Services.AddSingleton(identityServiceMock.Object);

        // Mock ICurrentUserService
        var currentUserServiceMock = mocker.GetMock<ICurrentUserService>();
        currentUserServiceMock.Setup(service => service.GetUserIdAsync())
            .Returns(Task.FromResult<string?>("1"));
        Services.AddSingleton(currentUserServiceMock.Object);

    }

    public override IRenderedFragment Render(RenderFragment renderFragment)
    {
        EnsureRootComponent();
        return base.Render(renderFragment);
    }

    public override IRenderedComponent<TComponent> Render<TComponent>(RenderFragment renderFragment)
    {
        EnsureRootComponent();
        return base.Render<TComponent>(renderFragment);
    }

    public override IRenderedComponent<TComponent> RenderComponent<TComponent>(params ComponentParameter[] parameters)
    {
        EnsureRootComponent();
        return base.RenderComponent<TComponent>(parameters);
    }

    public override IRenderedComponent<TComponent> RenderComponent<TComponent>(Action<ComponentParameterCollectionBuilder<TComponent>>? parameterBuilder)
    {
        EnsureRootComponent();
        return base.RenderComponent(parameterBuilder);
    }

    private void EnsureRootComponent()
    {
        if (rootComponent is not null) return;

        // add a Telerik Root Component to hold all Telerik components and other content
        rootComponent = (IRenderedComponent<TelerikRootComponent>)Renderer.RenderComponent<TelerikRootComponent>(new ComponentParameterCollection());

        // provide the Telerik Root Component to the child components that need it (the Telerik components)
        RenderTree.TryAdd<CascadingValue<TelerikRootComponent>>(p =>
        {
            p.Add(parameters => parameters.IsFixed, true);
            p.Add(parameters => parameters.Value, RootComponent.Instance);
        });
    }

    /// <summary>
    /// Logs the description of the calling method.
    /// </summary>
    /// <param name="callerName">The name of the calling method. This is automatically populated by the runtime.</param>
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
}
