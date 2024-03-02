using AgentAchieve.Infrastructure.Data;

namespace AgentAchieve.Infrastructure.UnitTests;

public class DatabaseFixture
{
    public ApplicationDbContext? Context { get; set; }

    public DatabaseFixture()
    {
    }

}
