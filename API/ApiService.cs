using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
namespace CSharpEmilyApp.API;

public abstract class ApiService
{
    private static readonly HttpClient _client = new();

    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    protected async Task<T> GetAsync<T>(string endpoint)
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config.ApiKey);
        var response = await _client.GetAsync(Config.ApiUrl + endpoint);
        var json = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        
        return JsonSerializer.Deserialize<T>(json, _options);
    }
}