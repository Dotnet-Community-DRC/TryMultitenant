using Microsoft.EntityFrameworkCore;
using TryMultiTenant.Data;

namespace TryMultiTenant.Services;

public class CurrentTenantService: ICurrentTenantService
{
    private readonly TenantDbContext _context;

    public CurrentTenantService(TenantDbContext context)
    {
        _context = context;
    }
    public string TenantId { get; set; }
    public string ConnectionString { get; set; }
    public async Task<bool> SetTenant(string tenant)
    {
        var tenantInfo = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenant);
        if (tenantInfo is null) throw new Exception("Invalid Tenant");
        TenantId = tenantInfo.Id;
        ConnectionString = tenantInfo.ConnectionString;
        
        return true;
    }
}