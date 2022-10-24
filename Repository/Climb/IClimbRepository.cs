using EngineMonitoring.Models;
using EngineMonitoring.Request.Climb.Payloads;
using EngineMonitoring.Responses;

namespace EngineMonitoring.Repository
{
    public interface IClimbRepository
    {
        Task<List<ClimbResponse>> GetClimbByOperation(int operationId);
        Task<Climb> PostClimb(ClimbPayload climbPayload);
    }
}