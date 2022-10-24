using EngineMonitoring.Models;
using EngineMonitoring.Request.TakeOff.Payloads;
using EngineMonitoring.Responses;

namespace EngineMonitoring.Repository
{
    public interface ITakeOffRepository
    {
        Task<List<TakeOffResponse>> GetTakeOffByOperation(int operationId);
        Task<TakeOff> PostTakeOff(TakeOffPayload takeOffPayload);
    }
}