using AgentAchieve.Infrastructure.Identity;
using AgentAchieve.Infrastructure.Services;
using System.Diagnostics.CodeAnalysis;

namespace AgentAchieve.Infrastructure.UnitTests
{
    public class BaseServiceTests : IClassFixture<DatabaseFixture>
    {
        private readonly BaseService<ApplicationUser> _service;
        private readonly DatabaseFixture _fixture;

        public BaseServiceTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _service = new BaseService<ApplicationUser>(_fixture.ApplicationUserRepository);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEntity_WhenEntityExists()
        {
            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId };
            _fixture.Context.Users.Add(entity);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _service.GetByIdAsync(entityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entityId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenEntityNotExists()
        {
            // Arrange 
            var nonExistentId = Guid.NewGuid().ToString();

            // Act
            var result = await _service.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task InsertAsync_AddsNewEntity()
        {
            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId, UserName = "TestUser2" };

            // Act
            await _service.InsertAsync(entity);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            var result = await _fixture.Context.Users.FindAsync(entityId);
            Assert.NotNull(result);
            Assert.Equal(entityId, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingEntity()
        {
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
            Assert.NotNull(result);
            Assert.Equal("UpdatedUser", result.UserName);
        }

        [Fact]
        public async Task DeleteAsync_RemovesExistingEntity()
        {
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
            Assert.Null(result);
        }
    }
}
