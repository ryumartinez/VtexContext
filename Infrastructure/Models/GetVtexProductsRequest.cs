using System.Web;

namespace Infrastructure.Models;

public class GetVtexProductsRequest
{
    private int? _from;
    private int? _to;
    private string _fullText;
    private readonly List<string> _filters = new();
    private string _order;

    public GetVtexProductsRequest WithFrom(int from)
    {
        if (from < 0 || from > 2500)
            throw new ArgumentOutOfRangeException(nameof(from), "Value must be between 0 and 2500.");

        _from = from;
        return this;
    }

    public GetVtexProductsRequest WithTo(int to)
    {
        if (to < 0 || to > 2500)
            throw new ArgumentOutOfRangeException(nameof(to), "Value must be between 0 and 2500.");

        _to = to;
        return this;
    }

    public GetVtexProductsRequest WithFullText(string fullText)
    {
        _fullText = fullText;
        return this;
    }

    // Filter by productId, appends "fq=productId:{productId}"
    public GetVtexProductsRequest WithProductId(int productId)
    {
        _filters.Add($"productId:{productId}");
        return this;
    }

    // Filter by category, takes array of category IDs for path like C:/a/b/
    public GetVtexProductsRequest WithCategory(params int[] categoryIds)
    {
        if (categoryIds == null || categoryIds.Length == 0)
            throw new ArgumentException("At least one category ID is required.", nameof(categoryIds));

        string categoriesPath = string.Join("/", categoryIds);
        _filters.Add($"C:/{categoriesPath}/");
        return this;
    }

    // Filter by brand IDs
    public GetVtexProductsRequest WithBrand(params int[] brandIds)
    {
        if (brandIds == null || brandIds.Length == 0)
            throw new ArgumentException("At least one brand ID is required.", nameof(brandIds));

        string brandsPath = string.Join("/", brandIds);
        _filters.Add($"B:/{brandsPath}/");
        return this;
    }

    // Filter by specification, e.g. specificationFilter_123:Blue
    public GetVtexProductsRequest WithSpecification(int specificationId, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Specification value cannot be null or empty.", nameof(value));

        _filters.Add($"specificationFilter_{specificationId}:{value}");
        return this;
    }

    // Filter by price range (min and max)
    public GetVtexProductsRequest WithPriceRange(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0 || maxPrice < 0)
            throw new ArgumentOutOfRangeException("Prices must be non-negative.");

        if (minPrice > maxPrice)
            throw new ArgumentException("minPrice cannot be greater than maxPrice.");

        _filters.Add($"P:[{minPrice} TO {maxPrice}]");
        return this;
    }

    // Filter by collection (product cluster id)
    public GetVtexProductsRequest WithCollection(int collectionId)
    {
        _filters.Add($"productClusterIds:{collectionId}");
        return this;
    }

    // Filter by SKU Id
    public GetVtexProductsRequest WithSkuId(int skuId)
    {
        _filters.Add($"skuId:{skuId}");
        return this;
    }

    // Filter by referenceId (RefId)
    public GetVtexProductsRequest WithReferenceId(string referenceId)
    {
        if(string.IsNullOrWhiteSpace(referenceId))
            throw new ArgumentException("ReferenceId cannot be null or empty.", nameof(referenceId));

        _filters.Add($"alternateIds_RefId:{referenceId}");
        return this;
    }

    // Filter by EAN13
    public GetVtexProductsRequest WithEan(string ean13)
    {
        if(string.IsNullOrWhiteSpace(ean13))
            throw new ArgumentException("EAN13 cannot be null or empty.", nameof(ean13));

        _filters.Add($"alternateIds_Ean:{ean13}");
        return this;
    }

    // Filter by availability at sales channel
    public GetVtexProductsRequest WithAvailabilityAtSalesChannel(int salesChannel, bool isAvailable)
    {
        _filters.Add($"isAvailablePerSalesChannel_{salesChannel}:{(isAvailable ? "1" : "0")}");
        return this;
    }

    // Filter by seller ID (excludes White Label Sellers)
    public GetVtexProductsRequest WithSellerId(int sellerId)
    {
        _filters.Add($"sellerId:{sellerId}");
        return this;
    }

    // Set sorting option, e.g. OrderByPriceDESC
    public GetVtexProductsRequest WithOrder(string order)
    {
        _order = order;
        return this;
    }

    // Generate the query string encoding all parameters, e.g. "_from=1&_to=50&ft=tv&fq=productId:1&O=OrderByPriceDESC"
    public string ToQueryString()
    {
        var queryParams = new List<string>();
        
        if (_from.HasValue)
            queryParams.Add("_from=" + _from.Value);

        if (_to.HasValue)
            queryParams.Add("_to=" + _to.Value);

        if (!string.IsNullOrWhiteSpace(_fullText))
            queryParams.Add("ft=" + HttpUtility.UrlEncode(_fullText));

        if (_filters.Count > 0)
        {
            // combine all filters into fq parameters, joined by space
            string fqValue = string.Join(" ", _filters);
            queryParams.Add("fq=" + HttpUtility.UrlEncode(fqValue));
        }

        if (!string.IsNullOrWhiteSpace(_order))
            queryParams.Add("O=" + HttpUtility.UrlEncode(_order));

        return string.Join("&", queryParams);
    }
}