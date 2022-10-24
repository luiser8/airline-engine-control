using System.ComponentModel.DataAnnotations;
using EngineMonitoring.Request.EngineDetail.Payloads;

namespace EngineMonitoring.Request.SwitchedOn.Payloads
{
    public class SwitchedOnPayload
    {
        [Required]
        public int OperationId { get; set; }
        [Required]
        public int EngineEventId { get; set; }
        public virtual ICollection<EngineDetailPayload> EngineDetailPayload { get; set; } = null!;
    }
}
