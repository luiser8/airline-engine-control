namespace EngineMonitoring.Responses
{
    public class EngineEventResponse
    {
        public int Id { get; set; }
        public int ForEvent { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}