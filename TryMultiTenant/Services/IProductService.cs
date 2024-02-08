using TryMultiTenant.Models;
using TryMultiTenant.Models.DTOs;

namespace TryMultiTenant.Services;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product CreateProduct(CreateProductRequest request);
    bool DeleteProduct(int id);
}