namespace EngineMonitoring.Responses
{
    public class EngineDetailResponse
    {
        public int? EngineNro { get; set; }
        public decimal? N1 { get; set; }
        public decimal? Egt { get; set; }
        public decimal? N2 { get; set; }
        public decimal? Ff { get; set; }
        public string? Vib { get; set; }
        public bool? EngineBleed { get; set; }
        public bool? InletAi { get; set; }
        public decimal? OilPressure { get; set; }
        public decimal? OilTemp { get; set; }
    }
}