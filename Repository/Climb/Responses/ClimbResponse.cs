namespace EngineMonitoring.Responses
{
    public class ClimbResponse
    {
        public List<DetailsInfoResponse> ?DetailsInfo { get; set; }
        public List<EngineDetailResponse> ?EngineDetailList { get; set; }
    }
}