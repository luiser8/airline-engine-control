using System.ComponentModel.DataAnnotations.Schema;

namespace EngineMonitoring.Models
{
    [Table("SwitchedOn")]
    public class SwitchedOn
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public int EngineEventId { get; set; }
        public virtual EngineEvent EngineEvent { get; set; } = null!;
        public virtual Operation Operation { get; set; } = null!;
    }
}