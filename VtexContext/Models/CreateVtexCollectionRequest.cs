namespace VtexContext.Models;

public class CreateVtexCollectionRequest
{
    public string Name { get; set; }  // Required

    public string Description { get; set; }  // Required, internal use only

    public bool Searchable { get; set; }  // Required

    public bool Highlight { get; set; }  // Required

    public string DateFrom { get; set; }  // Required, ISO date-time string

    public string DateTo { get; set; }  // Required, ISO date-time string
}