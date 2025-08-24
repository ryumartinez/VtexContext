using Infrastructure.Abstractions;
using Infrastructure.Domain;

namespace Infrastructure;

public class SqlProductRepository : IProductRepository
{
    public Task<IEnumerable<Product>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}