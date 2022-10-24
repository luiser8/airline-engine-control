using EngineMonitoring.Models;
using EngineMonitoring.Request.Cruise.Payloads;
using EngineMonitoring.Responses;

namespace EngineMonitoring.Repository
{
    public interface ICruiseRepository
    {
        Task<List<CruiseResponse>> GetCruiseByOperation(int operationId);
        Task<Cruise> PostCruise(CruisePayload cruisePayload);
    }
}