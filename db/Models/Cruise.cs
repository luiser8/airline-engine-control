using System.ComponentModel.DataAnnotations.Schema;

namespace EngineMonitoring.Models
{
    [Table("Cruise")]
    public class Cruise
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public int EngineEventId { get; set; }
        public int DetailsInfoId { get; set; }
        public virtual DetailsInfo DetailsInfo { get; set; } = null!;
        public virtual EngineEvent EngineEvent { get; set; } = null!;
        public virtual Operation Operation { get; set; } = null!;
    }
}