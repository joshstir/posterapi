using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PosterApi.Interfaces;

public class AuditInterceptor : SaveChangesInterceptor
{

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
		if (eventData.Context is not null)
		{
			UpdateAuditables(eventData.Context);
			Console.WriteLine("Saving changes");
		}	
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
		
		if (eventData.Context is not null)
		{
			UpdateAuditables(eventData.Context);
			Console.WriteLine("Saving changes async");
		}		
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private void UpdateAuditables(DbContext context)
    {
        var utcNow = DateTime.UtcNow;
		var entities = context.ChangeTracker.Entries<AuditEntity>();

		foreach (var entity in entities)
		{
			switch (entity.State)
			{
				case EntityState.Added:
					entity.Entity.CreatedAt = utcNow;
					entity.Entity.UpdatedAt = utcNow;
					entity.Entity.CreatedBy = "System";
					entity.Entity.UpdatedBy = "System";
					break;
				case EntityState.Modified:
					entity.Entity.UpdatedAt = utcNow;
					entity.Entity.UpdatedBy = "System";
					break;
			}
		}
    }
}
