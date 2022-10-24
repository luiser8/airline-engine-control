namespace EngineMonitoring.Models
{
    public partial class Climb
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
