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

        var results = await GetAsync<List<PlanningResponse>>($"/v1/orders/planning?startDate=2026-02-23&endDate=2026-02-23");
        
        return new PlanningApiResponse { Results = results ?? new() };    
    }
    
    public class PlanningApiResponse
    {
        public List<PlanningResponse> Results { get; set; } = new();
        // public GetPagination Pagination { get; set; } = new();
    }

    public class PlanningResponse
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Company { get; set; }
        public string? Description { get; set; }
        public object? QuotePrice { get; set; }
        public string? OrderStatus { get; set; }
        public string? DeliveryDate { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? Progress { get; set; }
        public int Amount { get; set; }
        public int MachineCount { get; set; }
        public List<PlanningMachine> Machines { get; set; } = new();
    }

    public class PlanningMachine
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string? Name { get; set; }
        public object? MachineId { get; set; }
        public string? MachinePlanningShiftType { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public double? TotalHours { get; set; }
        public List<PlanningEntry> Planning { get; set; } = new();
    }

    public class PlanningEntry
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public double Time { get; set; }
        public string? Uuid { get; set; }
        public int Priority { get; set; }
        public int Progress { get; set; }
        public string? Note { get; set; }
        public double? AmountProduced { get; set; }
        public List<PlanningPerson> Persons { get; set; } = new();
    }
    
    public class PlanningPerson
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}