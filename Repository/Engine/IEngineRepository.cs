using EngineMonitoring.Responses;

namespace EngineMonitoring.Repository
{
    public interface IEngineEventRepository
    {
        Task<List<EngineEventResponse>> GetEngineEvents();
        Task<List<EngineDetailResponse>> GetEngineDetails(int operationId, int engineEventId);
        Task<EngineEventResponse> GetEngineEvent(int forEvent);
    }
}