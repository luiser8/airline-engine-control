using System.ComponentModel.DataAnnotations;
using EngineMonitoring.Request.Climb.Payloads;
using EngineMonitoring.Request.Cruise.Payloads;
using EngineMonitoring.Request.Decline.Payloads;
using EngineMonitoring.Request.SwitchedOn.Payloads;
using EngineMonitoring.Request.TakeOff.Payloads;
using EngineMonitoring.Request.Crew.Payloads;

namespace EngineMonitoring.Request.Operations.Payloads
{
    public class OperationPayload
    {
        public int OperationPlotId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? UserEmail { get; set; }
        [Required]
        public string? RouteFrom { get; set; }
        [Required]
        public string? RouteTo { get; set; }
        [Required]
        public int Pax { get; set; }
        public int OAT { get; set; }
        public string? Pbarometrica { get; set; }
        public int Personal { get; set; }
        public string? Fuel { get; set; }
        public string? TOW { get; set; }
        public virtual SwitchedOnPayload SwitchedOnPayload { get; set; } = null!;
        public virtual TakeOffPayload TakeOffPayload { get; set; } = null!;
        public virtual ClimbPayload ClimbPayload { get; set; } = null!;
        public virtual CruisePayload CruisePayload { get; set; } = null!;
        public virtual DeclinePayload DeclinePayload { get; set; } = null!;
        public virtual CrewPayload CrewPayload { get; set; } = null!;
    }
}