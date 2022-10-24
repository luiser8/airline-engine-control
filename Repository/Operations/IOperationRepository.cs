using EngineMonitoring.Models;
using EngineMonitoring.Request.Operations.Payloads;
using EngineMonitoring.Responses;

namespace EngineMonitoring.Repository
{
    public interface IOperationsRepository
    {
        Task<List<OperationResponse>> GetOperations();
        Task<List<OperationsResponse>> GetOperation(int id);
        Task<List<OperationResponse>> GetOperationsFilters(string routes);
        Task<List<OperationResponse>> GetOperationsFilterByDateTime(DateTime date);
        Task<List<OperationsResponse>> GetOperationsByRoutes(string routeFrom, string routeTo);
        Task<Operation> PostOperation(OperationPayload operationPayload);
        Task<Operation> PutOperation(int id, Operation operation);
        Task<Operation> DeleteOperation(int id);
    }
}