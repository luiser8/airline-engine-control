using System.ComponentModel.DataAnnotations.Schema;
namespace EngineMonitoring.Models
{
    [Table("DetailsInfo")]
    public class DetailsInfo
    {
        public DetailsInfo()
        {
            Climbs = new HashSet<Climb>();
            Cruises = new HashSet<Cruise>();
            Declines = new HashSet<Decline>();
            TakeOffs = new HashSet<TakeOff>();
        }

        public int Id { get; set; }
        public decimal? Tat { get; set; }
        public decimal? Mach { get; set; }
        public decimal? Paltitude { get; set; }
        public string? PackValve1 { get; set; }
        public string? PackValve2 { get; set; }
        public bool? ConfAton { get; set; }
        public bool? ConfAtoff { get; set; }
        public string? IsolationValve { get; set; }
        public bool? WingAi { get; set; }
        public bool? ReducedPower { get; set; }
        public bool? RegularPower { get; set; }
        public virtual ICollection<Climb> Climbs { get; set; }
        public virtual ICollection<Cruise> Cruises { get; set; }
        public virtual ICollection<Decline> Declines { get; set; }
        public virtual ICollection<TakeOff> TakeOffs { get; set; }
    }
}