namespace EngineMonitoring.Responses
{
    public class DeclineResponse
    {
        public List<DetailsInfoResponse> ?DetailsInfo { get; set; }
        public List<EngineDetailResponse> ?EngineDetailList { get; set; }
    }
}