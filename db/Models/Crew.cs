using System.ComponentModel.DataAnnotations.Schema;

namespace EngineMonitoring.Models
{
    [Table("Crew")]
    public class Crew
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public string Captain { get; set; } = null!;
        public string Fo { get; set; } = null!;
        public string? Technical { get; set; }
        public virtual Operation Operation { get; set; } = null!;
    }
}