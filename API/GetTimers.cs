using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpEmilyApp.API;

public class GetTimers : ApiService
{
    public async Task<TimerApiResponse?> GetAsync() => await GetAsync<TimerApiResponse>("/v1/orders/timers");

    public class TimerApiResponse
    {
        public List<TimerResponse> Results { get; set; } = new();
        public GetPagination Pagination { get; set; } = new();
    }

    public class TimerResponse
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