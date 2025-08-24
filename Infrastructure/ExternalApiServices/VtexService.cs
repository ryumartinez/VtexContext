using System.Net.Http.Json;
using System.Text;
using System.Xml.Serialization;
using Infrastructure.Models;
using VtexContext.Models;

namespace Infrastructure.ExternalApiServices;

public class VtexService
{
     private readonly HttpClient _httpClient;
    private readonly string _vtexSearchBaseUrl = "";
    private readonly string _vtexCreateCollectionUrl = "";
    private readonly string _vtexAddProductosToCollectionUrl = "";
    private readonly int _suggestedProductsCollectionId = 162;

    public VtexService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<VtexProduct> GetProductByIdAsync(int id)
    {
        var request = new GetVtexProductsRequest().WithProductId(id);
        var response = await GetProductsAsync(request);
        return response.First();
    }

    public async Task<IEnumerable<VtexProduct>> GetProductsAsync(GetVtexProductsRequest request)
    {
        var queryString = request.ToQueryString();
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<VtexProduct>>($"{_vtexSearchBaseUrl}{queryString}");
        return result;
    }

    public async Task<IEnumerable<VtexProduct>> GetSuggestedProductsAsync()
    {
        var request = new GetVtexProductsRequest().WithCollection(_suggestedProductsCollectionId);
        var response = await GetProductsAsync(request);
        return response;
    }

    public async Task<VtexCollectionModel> CreateCollectionAsync(CreateVtexCollectionRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(_vtexCreateCollectionUrl, request);
        response.EnsureSuccessStatusCode();

        var collection = await response.Content.ReadFromJsonAsync<VtexCollectionModel>();
        return collection;
    }

    public async Task SetProductsToCollectionAsync(IEnumerable<VtexProduct> products)
    {
        var serializer = new XmlSerializer(typeof(IEnumerable<VtexProduct>));
   
        string xmlContent;
        using (var stringWriter = new StringWriter())
        {
            serializer.Serialize(stringWriter, products);
            xmlContent = stringWriter.ToString();
        }

        using var content = new MultipartFormDataContent();
        var xmlStringContent = new StringContent(xmlContent, Encoding.UTF8, "application/xml");
        content.Add(xmlStringContent, "file", "products.xml");
        var response = await _httpClient.PostAsync(_vtexAddProductosToCollectionUrl, content);

        response.EnsureSuccessStatusCode();
    }
}