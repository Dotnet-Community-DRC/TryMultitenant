namespace TryMultiTenant.Services;

public interface IMustHaveTenant
{
    public string TenantId { get; set; }
}
