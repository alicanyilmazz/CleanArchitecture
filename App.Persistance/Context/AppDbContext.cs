using App.Domain.Abstracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Context;
public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) 
    {    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<IAuditable>();
        foreach (var entry in entries)
        {
            if(entry.State == EntityState.Added)
                entry.Property(x=>x.CreatedAt).CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.Now;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
