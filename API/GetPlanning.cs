using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSharpEmilyApp.API;

public class GetPlanning : ApiService
{
    public async Task<PlanningApiResponse?> GetAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var start = (startDate ?? DateTime.Today).ToString("yyyy-MM-dd");
        var end = (endDate ?? DateTime.Today.AddDays(14)).ToString("yyyy-MM-dd");

        var results = await GetAsync<List<PlanningResponse>>($"/v1/orders/planning?startDate=2025-02-02&endDate=2025-02-03");
        
        return new PlanningApiResponse { Results = results ?? new() };    }
    
    public class PlanningApiResponse
    {
        public List<PlanningResponse> Results { get; set; } = new();
        // public GetPagination Pagination { get; set; } = new();
    }

    public class PlanningResponse
    {
        public int Id { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        
        public string? Number { get; set; }
        
        public int? QuotePrice { get; set; }
        
        public string? OrderStatus { get; set; }
    }
}