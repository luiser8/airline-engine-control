using System.ComponentModel.DataAnnotations.Schema;
namespace EngineMonitoring.Models
{
    [Table("Operation")]
    public class Operation
    {
        public Operation()
        {
            Climbs = new HashSet<Climb>();
            Crews = new HashSet<Crew>();
            Cruises = new HashSet<Cruise>();
            Declines = new HashSet<Decline>();
            EngineDetails = new HashSet<EngineDetail>();
            SwitchedOns = new HashSet<SwitchedOn>();
            TakeOffs = new HashSet<TakeOff>();
        }

        public int Id { get; set; }
        public int OperationPlotId { get; set; }
        public int UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? RouteFrom { get; set; }
        public string? RouteTo { get; set; }
        public int Pax { get; set; }
        public int? Oat { get; set; }
        public string? Pbarometrica { get; set; }
        public int? Personal { get; set; }
        public string? Fuel { get; set; }
        public string? Tow { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<Climb> Climbs { get; set; }
        public virtual ICollection<Crew> Crews { get; set; }
        public virtual ICollection<Cruise> Cruises { get; set; }
        public virtual ICollection<Decline> Declines { get; set; }
        public virtual ICollection<EngineDetail> EngineDetails { get; set; }
        public virtual ICollection<SwitchedOn> SwitchedOns { get; set; }
        public virtual ICollection<TakeOff> TakeOffs { get; set; }
    }
}