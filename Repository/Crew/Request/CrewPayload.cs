using System.ComponentModel.DataAnnotations;

namespace EngineMonitoring.Request.Crew.Payloads
{
    public class CrewPayload
    {
        public int OperationId { get; set; }
        [Required]
        public string Captain { get; set; } = null!;
        public string Fo { get; set; } = null!;
        public string? Technical { get; set; }
    }
}
