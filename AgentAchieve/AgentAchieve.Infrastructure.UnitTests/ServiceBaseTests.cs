using AgentAchieve.Infrastructure.Identity;
using AgentAchieve.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AgentAchieve.Infrastructure.UnitTests
{
    public class ServiceBaseTests : TestBase<ServiceBase<ApplicationUser>>, IClassFixture<DatabaseFixture>
    {
        private readonly ServiceBase<ApplicationUser> _service;
        private readonly DatabaseFixture _fixture;

        public ServiceBaseTests(DatabaseFixture fixture, ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _fixture = fixture;
            _service = new ServiceBase<ApplicationUser>(_fixture.ApplicationUserRepository);
        }

        [Trait("Description", "Verifies GetByIdAsync behavior for BaseService")]
        [Fact]
        public async Task GetByIdAsync_ReturnsEntity_WhenEntityExists()
        {
            LogDescription();

            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId };
            _fixture.Context.Users.Add(entity);
            await _fixture.Context.SaveChangesAsync();

            Logger.LogInformation("Test with entity ID: {Id}", entityId);

            // Act
            var result = await _service.GetByIdAsync(entityId);

            // Assert
            result.Should().NotBeNull("because an entity with this ID exists");
            result?.Id.Should().Be(entityId, "because the IDs should match");

            // Contextual Logging
            Logger.LogInformation("Entity Retrieved - ID: {Id}", result?.Id);
        }

        [Trait("Description", "Verifies GetByIdAsync returns null for non-existent entities")]
        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenEntityNotExists()
        {
            LogDescription();

            // Arrange 
            var nonExistentId = Guid.NewGuid().ToString();

            // Act
            var result = await _service.GetByIdAsync(nonExistentId);

            // Assert
            result.Should().BeNull("because no entity exists with this ID");

            // Contextual Logging
            Logger.LogInformation("Entity Not Found - ID: {Id}", nonExistentId);
        }

        [Trait("Description", "Verifies InsertAsync adds a new entity")]
        [Fact]
        public async Task InsertAsync_AddsNewEntity()
        {
            LogDescription();

            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId, UserName = "TestUser2" };

            // Act
            await _service.InsertAsync(entity);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            var result = await _fixture.Context.Users.FindAsync(entityId);
            result.Should().NotBeNull("because a new entity should be created");
            result?.Id.Should().Be(entityId, "because the IDs should match");

            // Contextual Logging
            Logger.LogInformation("New Entity Added - ID: {Id}", result?.Id);
        }

        [Trait("Description", "Verifies UpdateAsync modifies an existing entity")]
        [Fact]
        public async Task UpdateAsync_UpdatesExistingEntity()
        {
            LogDescription();

            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId, UserName = "TestUser3" };
            _fixture.Context.Users.Add(entity);
            await _fixture.Context.SaveChangesAsync();

            // Act
            entity.UserName = "UpdatedUser";
            await _service.UpdateAsync(entity);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            var result = await _fixture.Context.Users.FindAsync(entityId);
            result.Should().NotBeNull("because the entity should still exist");
            result?.UserName.Should().Be("UpdatedUser", "because the UserName was updated");

            // Contextual Logging
            Logger.LogInformation("Entity Updated - ID: {Id}, UserName: {UserName}", result?.Id, result?.UserName);
        }

        [Trait("Description", "Verifies DeleteAsync removes an existing entity")]
        [Fact]
        public async Task DeleteAsync_RemovesExistingEntity()
        {
            LogDescription();

            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId, UserName = "TestUser4" };
            _fixture.Context.Users.Add(entity);
            await _fixture.Context.SaveChangesAsync();

            // Act
            await _service.DeleteAsync(entityId);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            var result = await _fixture.Context.Users.FindAsync(entityId);
            result.Should().BeNull("because the entity should be deleted");

            // Contextual Logging
            Logger.LogInformation("Entity Deleted - ID: {Id}", entityId);
        }
    }
}
