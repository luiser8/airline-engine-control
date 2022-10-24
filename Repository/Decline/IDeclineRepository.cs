using EngineMonitoring.Models;
using EngineMonitoring.Request.Decline.Payloads;
using EngineMonitoring.Responses;

namespace EngineMonitoring.Repository
{
    public interface IDeclineRepository
    {
        Task<List<DeclineResponse>> GetDeclineByOperation(int operationId);
        Task<Decline> PostDecline(DeclinePayload declinePayload);
    }
}