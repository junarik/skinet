using Core.Entities;

namespace Core.Intrefaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);

    Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    
    Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    Task<IReadOnlyList<Product>> GetProductsAsync();
}