using Core.Entities;
using Core.Intrefaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructue.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _db;

    public ProductRepository(StoreContext db)
    {
        _db = db;
    }
    
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _db.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _db.ProductBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _db.ProductTypes.ToListAsync();
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _db.Products.ToListAsync();
    }
}