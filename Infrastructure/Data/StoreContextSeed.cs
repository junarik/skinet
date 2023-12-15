using System.Text.Json;
using Core.Entities;

namespace Infrastructue.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext _db)
    {
        if (!_db.ProductBrands.Any())
        {
            var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            _db.ProductBrands.AddRange(brands);
        }
        
        if (!_db.ProductTypes.Any())
        {
            var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            _db.ProductTypes.AddRange(types);
        }
        
        if (!_db.Products.Any())
        {
            var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            _db.Products.AddRange(products);
        }

        if (_db.ChangeTracker.HasChanges())
            await _db.SaveChangesAsync();
    }
    
    
}