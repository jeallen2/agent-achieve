using AgentAchieve.Infrastructure.Data;
using AgentAchieve.Infrastructure.Identity;
using AgentAchieve.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;


namespace AgentAchieve.Infrastructure.UnitTests
{
    public class DatabaseFixture
    {
        public ApplicationDbContext Context { get; private set; }
        public IRepository<ApplicationUser> ApplicationUserRepository { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB per run
                .Options;

            Context = new ApplicationDbContext(options);
            ApplicationUserRepository = new Repository<ApplicationUser>(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

    }
}
