namespace EngineMonitoring.Request.DetailsInfo.Payloads
{
    public class DetailsInfoPayload
    {
        public decimal TAT { get; set; }
        public decimal Mach { get; set; }
        public decimal PAltitude { get; set; }
        public string ?PackValve1 { get; set; }
        public string ?PackValve2 { get; set; }
        public bool ConfATon { get; set; }
        public bool ConfAToff { get; set; }
        public string ?IsolationValve { get; set; }
        public bool WingAI { get; set; }
        public bool ReducedPower { get; set; }
        public bool RegularPower { get; set; }
    }
}
