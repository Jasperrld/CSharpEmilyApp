using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharpEmilyApp.API;

public class GetPlanning
{
    private static readonly HttpClient _client = new();

    public async Task<PlanningApiResponse?> GetAsync()
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config.ApiKey);

        var response = await _client.GetAsync(Config.ApiUrl);
        var jsonRaw = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<PlanningApiResponse>(jsonRaw, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public class PlanningApiResponse
    {
        public List<PlanningResponse> Results { get; set; } = new();
        public Pagination Pagination { get; set; } = new();
    }

    public class Pagination
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int Total { get; set; }
    }

    public class PlanningResponse
    {
        public int Id { get; set; }
        public string? Start { get; set; }
        public string? End { get; set; }
        public string? TimerFor { get; set; }
        public User? User { get; set; }
        public Order? Order { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public Company? Company { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}