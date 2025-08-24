using Infrastructure.Domain;

namespace Infrastructure.Abstractions;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    
}