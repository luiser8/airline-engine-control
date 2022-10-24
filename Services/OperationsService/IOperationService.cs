using EngineMonitoring.Models;
using EngineMonitoring.Request.Operations.Payloads;
using EngineMonitoring.Responses;

namespace EngineMonitoring.Services
{
    public interface IOperationService
    {
        Task<List<OperationResponse>> GetOperations();
        Task<List<OperationResponse>> GetOperationsFilters(string routes);
        Task<List<OperationResponse>> GetOperationsFilterByDateTime(DateTime date);
        Task<List<OperationsResponse>> GetOperation(int id);
        Task<List<OperationsResponse>> GetOperationsByRoutes(string routeFrom, string routeTo);
        Task<Operation> PostOperation(OperationPayload operationPayload);
        Task<Operation> PutOperation(int id, Operation operation);
        Task<Operation> DeleteOperation(int id);
        void SendEmail(int id, string jsonData, OperationPayload operations);
    }
}