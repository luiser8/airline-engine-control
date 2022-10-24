using System.Text.Json.Serialization;

namespace EngineMonitoring.Responses
{
    public class OperationsResponse
    {
        public int Id { get; set; }
        public int OperationPlotId { get; set; }
        public string? UserEmail { get; set; }
        public string? RouteFrom { get; set; }
        public string? RouteTo { get; set; }
        public int Pax { get; set; }
        public int? Oat { get; set; }
        public string ?Pbarometrica { get; set; }
        public int? Personal { get; set; }
        public string? Fuel { get; set; }
        public string? Tow { get; set; }
        public string ?Date { get; set; }
        public List<SwitchedOnResponse> ?SwitchedOnList { get; set; }
        public List<TakeOffResponse> ?TakeOffList { get; set; }
        public List<ClimbResponse> ?ClimbList { get; set; }
        public List<CruiseResponse> ?CruiseList { get; set; }
        public List<DeclineResponse> ?DeclineList { get; set; }
        public List<CrewResponse> ?CrewList { get; set; }
    }
}