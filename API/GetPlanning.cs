using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpEmilyApp.API;

public class GetPlanning : ApiService
{
    public async Task<PlanningApiResponse?> GetAsync() => await GetAsync<PlanningApiResponse>("/v1/orders/planning");

    public class PlanningApiResponse
    {
        public List<PlanningResponse> Results { get; set; } = new();
        public GetPagination Pagination { get; set; } = new();
    }

    public class PlanningResponse
    {
        public int Id { get; set; }
    }
}