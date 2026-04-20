using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace CSharpEmilyApp.API;

public class GetPlanning
{
    private static readonly HttpClient _client = new();
    
    public async Task<PlanningResponse?> GetAsync()
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Config.ApiKey}");
        
        var response = await _client.GetAsync(Config.ApiUrl);
        var jsonRaw = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonRaw);
        response.EnsureSuccessStatusCode();

        
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PlanningResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
    }

    public class PlanningResponse
    {
        public string id { get; set; }
    }
}