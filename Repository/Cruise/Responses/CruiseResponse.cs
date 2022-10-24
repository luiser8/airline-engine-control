namespace EngineMonitoring.Responses
{
    public class CruiseResponse
    {
        public List<DetailsInfoResponse> ?DetailsInfo { get; set; }
        public List<EngineDetailResponse> ?EngineDetailList { get; set; }
    }
}