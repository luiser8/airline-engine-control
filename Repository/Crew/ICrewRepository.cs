using EngineMonitoring.Models;
using EngineMonitoring.Request.Crew.Payloads;
using EngineMonitoring.Responses;

namespace EngineMonitoring.Repository
{
    public interface ICrewRepository
    {
        Task<List<CrewResponse>> GetCrewByOperation(int operationId);
        Task<Crew> PostCrew(CrewPayload crewPayload);
    }
}