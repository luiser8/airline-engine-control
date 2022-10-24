using System.ComponentModel.DataAnnotations.Schema;

namespace EngineMonitoring.Models
{
    [Table("EngineDetail")]
    public class EngineDetail
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public int? EngineEventId { get; set; }
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
        public virtual EngineEvent? EngineEvent { get; set; }
        public virtual Operation Operation { get; set; } = null!;
    }
}