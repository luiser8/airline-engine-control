using System.ComponentModel.DataAnnotations;
using EngineMonitoring.Request.DetailsInfo.Payloads;
using EngineMonitoring.Request.EngineDetail.Payloads;

namespace EngineMonitoring.Request.Climb.Payloads
{
    public class ClimbPayload
    {
        public int OperationId { get; set; }
        [Required]
        public int EngineEventId { get; set; }
        [Required]
        public int DetailsInfoId { get; set; }
        public virtual DetailsInfoPayload DetailsInfoPayload { get; set; } = null!;
        public virtual ICollection<EngineDetailPayload> EngineDetailPayload { get; set; } = null!;
    }
}
