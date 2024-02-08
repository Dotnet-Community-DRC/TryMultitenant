using Microsoft.EntityFrameworkCore;
using TryMultiTenant.Models;
using TryMultiTenant.Services;

namespace TryMultiTenant.Data;

public class AppDbContext: DbContext
{
    private readonly ICurrentTenantService _currentTenantService;
    public string CurrentTenantId { get; set; }
    public string CurrentTenantConnectionString { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options, 
        ICurrentTenantService currentTenantService): base(options)
    {
        _currentTenantService = currentTenantService;
        CurrentTenantId = _currentTenantService.TenantId;
        CurrentTenantConnectionString = _currentTenantService.ConnectionString;
    }

    public DbSet<Product> Products { get; set; }
    // public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasQueryFilter(y => y.TenantId == CurrentTenantId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var tenantConnectionString = CurrentTenantConnectionString;
        if (!string.IsNullOrEmpty(tenantConnectionString)) 
        {
            _ = optionsBuilder.UseSqlite(tenantConnectionString);
        }
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
        {
            entry.Entity.TenantId = entry.State switch
            {
                EntityState.Added => CurrentTenantId,
                EntityState.Modified => CurrentTenantId,
                _ => entry.Entity.TenantId
            };
        }

        var result = base.SaveChanges();
        return result;
    }
}