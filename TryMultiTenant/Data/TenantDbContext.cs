using Microsoft.EntityFrameworkCore;
using TryMultiTenant.Models;

namespace TryMultiTenant.Data;

public class TenantDbContext: DbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options): base(options)
    {
        
    }

    public DbSet<Tenant> Tenants { get; set; }
}