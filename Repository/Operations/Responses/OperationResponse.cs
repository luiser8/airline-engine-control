namespace EngineMonitoring.Responses
{
    public class OperationResponse
    {
        public int Id { get; set; }
        public int OperationPlotId { get; set; }
        public string? UserEmail { get; set; }
        public string? RouteFrom { get; set; }
        public string? RouteTo { get; set; }
        public int Pax { get; set; }
        public int? Oat { get; set; }
        public string? Pbarometrica { get; set; }
        public int? Personal { get; set; }
        public string? Fuel { get; set; }
        public string? Tow { get; set; }
        public DateTime CreationDate { get; set; }
    }
}