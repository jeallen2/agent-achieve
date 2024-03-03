using AgentAchieve.Core.Common;
using AgentAchieve.Infrastructure.Features.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

/// <summary>
/// Interceptor for auditing changes made to entities implementing the <see cref="BaseAuditableEntity{TKey}"/> interface.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AuditableEntityInterceptor"/> class.
/// </remarks>
/// <param name="currentUserService">The current user service.</param>
public class AuditableEntityInterceptor(ICurrentUserService currentUserService) : ISaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService = currentUserService;

    /// <inheritdoc/>
    public Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await UpdateEntities(eventData.Context);
        return result;
    }

    private async Task UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var userId = await _currentUserService.GetUserIdAsync();

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity<int>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.CreatedBy = userId;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.Now;
                    entry.Entity.LastModifiedBy = userId; 
                    break;
            }
        }
    }
}
