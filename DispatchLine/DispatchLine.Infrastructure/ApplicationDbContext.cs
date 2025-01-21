using DispatchLine.Domain.Common;
using DispatchLine.Domain.Entities;
using DispatchLine.Domain.Entities.Identity;
using DispatchLine.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DispatchLine.Infrastructure;

internal class ApplicationDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

        return new ApplicationDbContext(builder.Options);
    }
}

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
    }

    public override int SaveChanges()
    {
        UpdateEntities();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateEntities();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateEntities()
    {
        var utcNow = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity is AuditableEntity auditableEntity)
                    {
                        auditableEntity.Created = utcNow;
                        auditableEntity.LastModified = utcNow;
                    }

                    break;

                case EntityState.Modified:
                    if (entry.Entity is AuditableEntity auditableEntityModified)
                    {
                        auditableEntityModified.LastModified = utcNow;
                    }

                    break;

                case EntityState.Deleted:
                    if (entry.Entity is ISoftDeleteEntity softDeleteEntity)
                    {
                        softDeleteEntity.IsDeleted = true;
                        softDeleteEntity.DeletedAt = utcNow;

                        entry.State = EntityState.Modified;
                    }

                    break;
            }
        }
    }
}