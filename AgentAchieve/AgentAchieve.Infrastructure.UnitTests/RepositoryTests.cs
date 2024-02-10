namespace AgentAchieve.Infrastructure.UnitTests
{
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using AgentAchieve.Infrastructure.Services;
    using AgentAchieve.Infrastructure.Data;
    using System.Threading.Tasks;
    using AgentAchieve.Infrastructure.Identity;
    using Microsoft.Extensions.Options;

    public class RepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly ApplicationDbContext _context;
        public RepositoryTests(DatabaseFixture fixture)
        {
            _context = fixture.Context;
            _repository = fixture.ApplicationUserRepository;
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEntity_WhenEntityExists()
        {
            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId };
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(entityId);

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
            var result = await _repository.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
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
            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
            Assert.Contains(entity1, result);
            Assert.Contains(entity2, result);
        }

        [Fact]
        public async Task InsertAsync_AddsNewEntity()
        {
            // Arrange
            var entityId = Guid.NewGuid().ToString();
            var entity = new ApplicationUser { Id = entityId };

            // Act
            await _repository.InsertAsync(entity);
            await _context.SaveChangesAsync();

            // Assert
            var result = await _repository.GetByIdAsync(entityId);
            Assert.NotNull(result);
            Assert.Equal(entityId, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingEntity()
        {
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
            Assert.NotNull(result);
            Assert.Equal("NewName", result.UserName);
        }

        [Fact]
        public async Task DeleteAsync_RemovesExistingEntity()
        {
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
            Assert.Null(result);
        }
    }
}