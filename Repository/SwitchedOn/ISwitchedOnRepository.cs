using EngineMonitoring.Models;
using EngineMonitoring.Request.SwitchedOn.Payloads;
using EngineMonitoring.Responses;

namespace EngineMonitoring.Repository
{
    public interface ISwitchedOnRepository
    {
        Task<List<SwitchedOnResponse>> GetSwitchedOnByOperation(int operationId);
        Task<SwitchedOn> PostSwitchedOn(SwitchedOnPayload switchedOnPayload);
    }
}