using EngineMonitoring.Responses;

namespace EngineMonitoring.Services
{
    public interface IEngineEventService
    {
        Task<List<EngineEventResponse>> GetEngineEvents();
        Task<EngineEventResponse> GetEngineEvent(int id);
    }
}