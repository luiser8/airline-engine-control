namespace EngineMonitoring.Responses
{
    public class TakeOffResponse
    {
        public List<DetailsInfoResponse> ?DetailsInfo { get; set; }
        public List<EngineDetailResponse> ?EngineDetailList { get; set; }
    }
}