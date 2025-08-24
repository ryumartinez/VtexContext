namespace VtexContext.Models;

public class VtexCollectionModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Searchable { get; set; }

    public bool Highlight { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int TotalProducts { get; set; }

    public string Type { get; set; }
}