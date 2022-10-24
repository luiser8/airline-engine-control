namespace EngineMonitoring.Responses
{
    public class DetailsInfoResponse
    {
        public decimal? Tat { get; set; }
        public decimal? Mach { get; set; }
        public decimal? Paltitude { get; set; }
        public string? PackValve1 { get; set; }
        public string? PackValve2 { get; set; }
        public bool? ConfAton { get; set; }
        public bool? ConfAtoff { get; set; }
        public string? IsolationValve { get; set; }
        public bool? WingAi { get; set; }
        public bool? ReducedPower { get; set; }
        public bool? RegularPower { get; set; }
    }
}