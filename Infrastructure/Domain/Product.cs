namespace Infrastructure.Domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Sku { get; set; }
    public string Category { get; set; }
    public ICollection<ProductCollection> ProductCollections { get; set; }
}