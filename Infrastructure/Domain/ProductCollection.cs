namespace Infrastructure.Domain;

public class ProductCollection
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int TotalProducts { get; set; }
    
    public ICollection<Product> Products { get; set; }
}