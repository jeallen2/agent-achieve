namespace AgentAchieve.Infrastructure.UnitTests
{
    using Xunit;
    using AgentAchieve.Infrastructure.Services;
    using AgentAchieve.Infrastructure.Data;
    using System.Threading.Tasks;
    using AgentAchieve.Infrastructure.Identity;
    using Xunit.Abstractions;
    using FluentAssertions;
    using Microsoft.Extensions.Logging;

    public class RepositoryTests : TestBase<IRepository<ApplicationUser>>, IClassFixture<DatabaseFixture>
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly ApplicationDbContext _context;
        public RepositoryTests(DatabaseFixture fixture, ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _context = fixture.Context;
            _repository = fixture.ApplicationUserRepository;
        }

        [Trait("Description", "Verifies GetByIdAsync returns entity when it exists")]
        [Fact]
        public async Task GetByIdAsync_ReturnsEntity_WhenEntityExists()
        {
            LogDescription();

            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId };
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            Logger.LogInformation("Test with entity ID: {Id}", entityId);

            // Act
            var result = await _repository.GetByIdAsync(entityId);

            // Assert
            result.Should().NotBeNull("because an entity with this ID exists");
            result?.Id.Should().Be(entityId, "because the IDs should match");

            // Contextual Logging
            Logger.LogInformation("Entity Retrieved - ID: {Id}", result?.Id);
        }

        [Trait("Description", "Verifies GetByIdAsync returns null when entity does not exist")]
        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenEntityNotExists()
        {
            LogDescription();

            // Arrange 
            var nonExistentId = Guid.NewGuid().ToString();

            // Act
            var result = await _repository.GetByIdAsync(nonExistentId);

            // Assert
            result.Should().BeNull("because no entity exists with this ID");

            // Contextual Logging
            Logger.LogInformation("Entity Not Found - ID: {Id}", nonExistentId);
        }

        [Trait("Description", "Verifies GetAllAsync returns all entities")]
        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            LogDescription();

            // Arrange
            var entityId1 = Guid.NewGuid().ToString();
            var entityId2 = Guid.NewGuid().ToString();
            var entity1 = new ApplicationUser { Id = entityId1 };
            var entity2 = new ApplicationUser { Id = entityId2 };
            _context.Users.AddRange(entity1, entity2);
            await _context.SaveChangesAsync();

            // Act
            var result = _repository.GetAll();

            // Assert
            result.Should().NotBeNull("because entities were added");
            result.Count().Should().BeGreaterOrEqualTo(2, "because at least two entities were added");
            result.Should().Contain(entity1, "because entity1 was added");
            result.Should().Contain(entity2, "because entity2 was added");

            // Contextual Logging
            Logger.LogInformation("Entities Retrieved - Count: {Count}", result.Count());
        }

        [Trait("Description", "Verifies InsertAsync adds a new entity")]
        [Fact]
        public async Task InsertAsync_AddsNewEntity()
        {
            LogDescription();

            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId };

            // Act
            await _repository.InsertAsync(entity);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _repository.GetByIdAsync(entityId);
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
            var entity = new ApplicationUser { Id = entityId, UserName = "OldName" };
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            // Act
            entity.UserName = "NewName";
            await _repository.UpdateAsync(entity);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _repository.GetByIdAsync(entityId);
            result.Should().NotBeNull("because the entity should still exist");
            result?.UserName.Should().Be("NewName", "because the UserName was updated");

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
            var entity = new ApplicationUser { Id = entityId };
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteAsync(entityId);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _repository.GetByIdAsync(entityId);
            result.Should().BeNull("because the entity should be deleted");

            // Contextual Logging
            Logger.LogInformation("Entity Deleted - ID: {Id}", entityId);
        }
    }
}