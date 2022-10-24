using System.ComponentModel.DataAnnotations.Schema;

namespace EngineMonitoring.Models
{
    [Table("EngineEvent")]
    public class EngineEvent
    {
        public EngineEvent()
        {
            Climbs = new HashSet<Climb>();
            Cruises = new HashSet<Cruise>();
            Declines = new HashSet<Decline>();
            EngineDetails = new HashSet<EngineDetail>();
            SwitchedOns = new HashSet<SwitchedOn>();
            TakeOffs = new HashSet<TakeOff>();
        }

        public int Id { get; set; }
        public int ForEvent { get; set; }
        public string EventName { get; set; } = null!;
        public virtual ICollection<Climb> Climbs { get; set; }
        public virtual ICollection<Cruise> Cruises { get; set; }
        public virtual ICollection<Decline> Declines { get; set; }
        public virtual ICollection<EngineDetail> EngineDetails { get; set; }
        public virtual ICollection<SwitchedOn> SwitchedOns { get; set; }
        public virtual ICollection<TakeOff> TakeOffs { get; set; }
    }
}