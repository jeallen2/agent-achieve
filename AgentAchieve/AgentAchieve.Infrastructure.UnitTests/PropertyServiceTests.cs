using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Properties; // Adjust namespace if needed
using AgentAchieve.Infrastructure.Features.Propertys;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace AgentAchieve.Infrastructure.UnitTests;
public class PropertyServiceTests(ITestOutputHelper outputHelper, DatabaseFixture dbFixture) : TestBase<PropertyService>(outputHelper, dbFixture)
{
    [Trait("Description", "Verifies that all properties are returned")]
    [Fact]
    public async Task GetAllDtoAsync_ShouldReturnAllProperties()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating PropertyService and adding properties");
        var propertyService = CreatePropertyService();
        var property1 = new Property("123 Main St", "Anytown", "CA", "12345") { YearBuilt = new DateTime(2000) };
        var property2 = new Property("456 Elm St", "Othertown", "NY", "54321") { YearBuilt = new DateTime(1985) };

        await AddAsync(property1);
        await AddAsync(property2);

        // Act
        Logger.LogInformation("Calling GetAllDtoAsync");
        var result = await propertyService.GetAllDtoAsync();

        // Assert
        Logger.LogInformation("Asserting that all properties are returned");
        result.Should().HaveCount(2);
        Logger.LogInformation("All properties returned successfully");
    }

    [Trait("Description", "Verifies that the correct property is returned by ID")]
    [Fact]
    public async Task GetDtoByIdAsync_ShouldReturnCorrectProperty()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating PropertyService and adding a property");
        var propertyService = CreatePropertyService();
        var property = new Property("123 Main St", "Anytown", "CA", "12345") { YearBuilt = new DateTime(2000) };
        await AddAsync(property);

        // Act
        Logger.LogInformation("Calling GetDtoByIdAsync");
        var result = await propertyService.GetDtoByIdAsync(property.Id);

        // Assert
        Logger.LogInformation("Asserting that the correct property is returned");
        result.Should().NotBeNull();
        result!.Id.Should().Be(property.Id);
        result.Street.Should().Be(property.Street);
        result.City.Should().Be(property.City);
        result.State.Should().Be(property.State);
        result.ZipCode.Should().Be(property.ZipCode);
        result.YearBuilt.Should().Be(property.YearBuilt);
        Logger.LogInformation("Correct property returned successfully");
    }

    [Trait("Description", "Verifies that a property is added to the database")]
    [Fact]
    public async Task CreatePropertyAsync_ShouldAddPropertyToDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating PropertyService and a property DTO");
        var propertyService = CreatePropertyService();
        var propertyDto = new PropertyDto
        {
            Street = "123 Main St",
            City = "Anytown",
            State = "CA",
            ZipCode = "12345",
            Country = "USA",
            YearBuilt = new DateTime(2000),
            Bedrooms = 3,
            FullBathrooms = 2,
            HalfBathrooms = 1,
            SquareFootage = 2000,
            NumberOfLevels = 2,
            LotSize = 5000,
            PropertyType = PropertyType.SingleFamilyHome,
            Description = "Test property"
        };

        // Act
        Logger.LogInformation("Calling CreatePropertyAsync");
        var result = await propertyService.CreatePropertyAsync(propertyDto);

        // Assert
        Logger.LogInformation("Asserting that the property is added to the database");
        result.Should().NotBeNull();
        var dbProperty = await FindAsync<Property>(result.Id);
        dbProperty.Should().NotBeNull();
        dbProperty!.Street.Should().Be(propertyDto.Street);
        dbProperty.City.Should().Be(propertyDto.City);
        dbProperty.State.Should().Be(propertyDto.State);
        dbProperty.ZipCode.Should().Be(propertyDto.ZipCode);
        dbProperty.Country.Should().Be(propertyDto.Country);
        dbProperty.YearBuilt.Should().Be(propertyDto.YearBuilt);
        dbProperty.Bedrooms.Should().Be(propertyDto.Bedrooms);
        dbProperty.FullBathrooms.Should().Be(propertyDto.FullBathrooms);
        dbProperty.HalfBathrooms.Should().Be(propertyDto.HalfBathrooms);
        dbProperty.SquareFootage.Should().Be(propertyDto.SquareFootage);
        dbProperty.NumberOfLevels.Should().Be(propertyDto.NumberOfLevels);
        dbProperty.LotSize.Should().Be(propertyDto.LotSize);
        dbProperty.PropertyType.Should().Be(propertyDto.PropertyType);
        dbProperty.Description.Should().Be(propertyDto.Description);
        Logger.LogInformation("Property added successfully");
    }

    [Trait("Description", "Verifies that a property is updated in the database")]
    [Fact]
    public async Task UpdatePropertyAsync_ShouldUpdatePropertyInDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating PropertyService, adding a property, and creating a property DTO");
        var propertyService = CreatePropertyService();
        var property = new Property("123 Main St", "Anytown", "CA", "12345") { YearBuilt = new DateTime(2000) };
        await AddAsync(property);

        // update and change fields
        var propertyDto = new PropertyDto
        {
            Id = property.Id,
            Street = "456 Elm St", 
            City = "Othertown",
            YearBuilt = new DateTime(2022),
            Bedrooms = 4
        };

        // Act
        Logger.LogInformation("Calling UpdatePropertyAsync");
        var result = await propertyService.UpdatePropertyAsync(propertyDto);

        // Assert
        Logger.LogInformation("Asserting that the property is updated in the database");
        result.Should().NotBeNull();
        var dbProperty = await FindAsync<Property>(result.Id);
        dbProperty.Should().NotBeNull();
        dbProperty!.Street.Should().Be(propertyDto.Street);
        dbProperty.City.Should().Be(propertyDto.City);
        dbProperty.YearBuilt.Should().Be(propertyDto.YearBuilt);
        dbProperty.Bedrooms.Should().Be(propertyDto.Bedrooms);
        Logger.LogInformation("Property updated successfully");
    }

    [Trait("Description", "Verifies that a property is removed from the database")]
    [Fact]
    public async Task DeletePropertyAsync_ShouldRemovePropertyFromDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating PropertyService and adding a property");
        var propertyService = CreatePropertyService();
        var property = new Property("123 Main St", "Anytown", "CA", "12345") { YearBuilt = new DateTime(2000) };
        await AddAsync(property);

        // Act
        Logger.LogInformation("Calling DeletePropertyAsync");
        await propertyService.DeletePropertyAsync(property.Id);

        // Assert
        Logger.LogInformation("Asserting that the property is removed from the database");
        var dbProperty = await FindAsync<Property>(property.Id);
        dbProperty.Should().BeNull();
        Logger.LogInformation("Property removed successfully");
    }
}
