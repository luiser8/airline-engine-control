using System.ComponentModel.DataAnnotations;
using EngineMonitoring.Request.DetailsInfo.Payloads;
using EngineMonitoring.Request.EngineDetail.Payloads;

namespace EngineMonitoring.Request.Cruise.Payloads
{
    public class CruisePayload
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
