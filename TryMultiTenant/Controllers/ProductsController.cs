using Microsoft.AspNetCore.Mvc;
using TryMultiTenant.Models.DTOs;
using TryMultiTenant.Services;

namespace TryMultiTenant.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var products = _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpPost]
    public IActionResult Post(CreateProductRequest request)
    {
        var product = _productService.CreateProduct(request);
        return Ok(product);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _productService.DeleteProduct(id);
        return Ok(product);
    }
}