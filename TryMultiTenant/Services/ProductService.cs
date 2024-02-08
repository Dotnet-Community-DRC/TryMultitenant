using TryMultiTenant.Data;
using TryMultiTenant.Models;
using TryMultiTenant.Models.DTOs;

namespace TryMultiTenant.Services;

public class ProductService: IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }
    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Product CreateProduct(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.Add(product);
        _context.SaveChanges();

        return product;
    }

    public bool DeleteProduct(int id)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == id);

        if (product is null) return false;
        
        _context.Remove(product);
        _context.SaveChanges();
        
        return true;
    }
}