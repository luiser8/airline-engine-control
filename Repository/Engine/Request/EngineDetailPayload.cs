using System.ComponentModel.DataAnnotations;

namespace EngineMonitoring.Request.EngineDetail.Payloads
{
    public class EngineDetailPayload
    {
        [Required]
        public int OperationId { get; set; }
        [Required]
        public int EngineNro { get; set; }
        public decimal N1 { get; set; }
        public decimal N2 { get; set; }
        public decimal EGT { get; set; }
        public decimal FF { get; set; }
        public string ?Vib { get; set; }
        public bool EngineBleed { get; set; }
        public bool InletAI { get; set; }
        public decimal OilPressure { get; set; }
        public decimal OilTemp { get; set; }
    }
}
