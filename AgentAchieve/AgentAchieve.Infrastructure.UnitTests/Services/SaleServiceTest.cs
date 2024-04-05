using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Sales;
using FluentAssertions;
using AgentAchieve.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AgentAchieve.Infrastructure.UnitTests.Services;
public class SaleServiceTests(ITestOutputHelper outputHelper, DatabaseFixture dbFixture) : TestBase<SaleService>(outputHelper, dbFixture)
{
    [Trait("Description", "Verifies that all sales are returned")]
    [Fact]
    public async Task GetAllDtoAsync_ShouldReturnAllSales()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SaleService and adding properties, clients, and sales");
        var saleService = CreateSaleService();

        var applicationUser = new ApplicationUser { Id = "Agent1" };
        var applicationUser2 = new ApplicationUser { Id = "Agent2" };
        await AddAsync(applicationUser);
        await AddAsync(applicationUser2);

        var property1 = new Property("123 Main St", "City1", "ST", "12345");
        var property2 = new Property("456 Oak St", "City2", "ST", "67890");
        await AddAsync(property1);
        await AddAsync(property2);

        var client1 = new Client("John", "Doe", "555-555-5555");
        var client2 = new Client("Jane", "Smith", "555-555-5556");
        await AddAsync(client1);
        await AddAsync(client2);

        var sale1 = new Sale(applicationUser.Id) { PropertyId = property1.Id, ClientId = client1.Id, SaleType = SaleType.Buyer, ClosingDate = DateTime.Now, SalePrice = 100000, CommissionRate = 0.05m };
        var sale2 = new Sale(applicationUser2.Id) { PropertyId = property2.Id, ClientId = client2.Id, SaleType = SaleType.Seller, ClosingDate = DateTime.Now, SalePrice = 200000, CommissionRate = 0.05m };

        await AddAsync(sale1);
        await AddAsync(sale2);

        // Act
        Logger.LogInformation("Calling GetAllDtoAsync");
        var result = await saleService.GetAllDtoAsync();

        // Assert
        Logger.LogInformation("Asserting that all sales are returned");
        result.Should().HaveCount(2);
        Logger.LogInformation("All sales returned successfully");
    }

    [Trait("Description", "Verifies that the correct sale is returned by ID")]
    [Fact]
    public async Task GetDtoByIdAsync_ShouldReturnCorrectSale()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SaleService and adding a sale");
        var saleService = CreateSaleService();

        var applicationUser = new ApplicationUser { Id = "Agent1" };
        await AddAsync(applicationUser);

        var property1 = new Property("123 Main St", "City1", "ST", "12345");
        await AddAsync(property1);

        var client1 = new Client("John", "Doe", "555-555-5555");
        await AddAsync(client1);

        var sale = new Sale(applicationUser.Id) { PropertyId = property1.Id, ClientId = client1.Id, SaleType = SaleType.Buyer, ClosingDate = DateTime.Now, SalePrice = 100000, CommissionRate = 0.05m };
        await AddAsync(sale);

        // Act
        Logger.LogInformation("Calling GetDtoByIdAsync");
        var result = await saleService.GetDtoByIdAsync(sale.Id);

        // Assert
        Logger.LogInformation("Asserting that the correct sale is returned");
        result.Should().NotBeNull();
        result!.Id.Should().Be(sale.Id);
        result.OwnedById.Should().Be(sale.OwnedById);
        result.PropertyId.Should().Be(sale.PropertyId);
        result.ClientId.Should().Be(sale.ClientId);
        result.SaleType.Should().Be(sale.SaleType);
        result.ClosingDate.Should().Be(sale.ClosingDate);
        result.SalePrice.Should().Be(sale.SalePrice);
        result.CommissionRate.Should().Be(sale.CommissionRate);
        Logger.LogInformation("Correct sale returned successfully");
    }

    [Trait("Description", "Verifies that a sale is added to the database")]
    [Fact]
    public async Task CreateSaleAsync_ShouldAddSaleToDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SaleService and a sale DTO");
        var saleService = CreateSaleService();
        var saleDto = new SaleDto
        {
            OwnedById = "Agent1",
            PropertyId = 1,
            ClientId = 1,
            SaleType = SaleType.Buyer,
            ClosingDate = DateTime.Now,
            SalePrice = 100000,
            CommissionRate = 0.05m
        };

        // Act
        Logger.LogInformation("Calling CreateSaleAsync");
        var result = await saleService.CreateSaleAsync(saleDto);

        // Assert
        Logger.LogInformation("Asserting that the sale is added to the database");
        result.Should().NotBeNull();
        var dbSale = await FindAsync<Sale>(result.Id);
        dbSale.Should().NotBeNull();
        dbSale!.OwnedById.Should().Be(saleDto.OwnedById);
        dbSale.PropertyId.Should().Be(saleDto.PropertyId.Value);
        dbSale.ClientId.Should().Be(saleDto.ClientId.Value);
        dbSale.SaleType.Should().Be(saleDto.SaleType);
        dbSale.ClosingDate.Should().Be(saleDto.ClosingDate);
        dbSale.SalePrice.Should().Be(saleDto.SalePrice.Value);
        dbSale.CommissionRate.Should().Be(saleDto.CommissionRate.Value);

        Logger.LogInformation("Sale added successfully");
    }

    [Trait("Description", "Verifies that a sale is updated in the database")]
    [Fact]
    public async Task UpdateSaleAsync_ShouldUpdateSaleInDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SaleService and a sale DTO");
        var saleService = CreateSaleService();
        var sale = new Sale("Agent1") { PropertyId = 1, ClientId = 1, SaleType = SaleType.Buyer, ClosingDate = DateTime.Now, SalePrice = 100000, CommissionRate = 0.05m };
        await AddAsync(sale);
        var saleDto = new SaleDto
        {
            Id = sale.Id,
            OwnedById = "Agent2",
            PropertyId = 2,
            ClientId = 2,
            SaleType = SaleType.Seller,
            ClosingDate = DateTime.Now.AddDays(1),
            SalePrice = 200000,
            CommissionRate = 0.06m
        };

        // Act
        Logger.LogInformation("Calling UpdateSaleAsync");
        var result = await saleService.UpdateSaleAsync(saleDto);

        // Assert
        Logger.LogInformation("Asserting that the sale is updated in the database");
        result.Should().NotBeNull();
        var dbSale = await FindAsync<Sale>(result.Id);
        dbSale.Should().NotBeNull();
        dbSale!.OwnedById.Should().Be(saleDto.OwnedById);
        dbSale.PropertyId.Should().Be(saleDto.PropertyId.Value);
        dbSale.ClientId.Should().Be(saleDto.ClientId.Value);
        dbSale.SaleType.Should().Be(saleDto.SaleType);
        dbSale.ClosingDate.Should().Be(saleDto.ClosingDate);
        dbSale.SalePrice.Should().Be(saleDto.SalePrice.Value);
        dbSale.CommissionRate.Should().Be(saleDto.CommissionRate.Value);

        Logger.LogInformation("Sale updated successfully");
    }

    [Trait("Description", "Verifies that a sale is removed from the database")]
    [Fact]
    public async Task DeleteSaleAsync_ShouldRemoveSaleFromDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating SaleService and a sale");
        var saleService = CreateSaleService();
        var sale = new Sale("Agent1") { PropertyId = 1, ClientId = 1, SaleType = SaleType.Buyer, ClosingDate = DateTime.Now, SalePrice = 100000, CommissionRate = 0.05m };
        await AddAsync(sale);

        // Act
        Logger.LogInformation("Calling DeleteSaleAsync");
        await saleService.DeleteSaleAsync(sale.Id);

        // Assert
        Logger.LogInformation("Asserting that the sale is removed from the database");
        var dbSale = await FindAsync<Sale>(sale.Id);
        dbSale.Should().BeNull();

        Logger.LogInformation("Sale removed successfully");
    }
}
