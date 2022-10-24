using EngineMonitoring.Models;
using EngineMonitoring.Repository;
using EngineMonitoring.Request.Operations.Payloads;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EngineMonitoring.Services
{
    public class OperationService : IOperationService
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        private readonly IOperationsRepository _operationsRepository;
        private readonly ISwitchedOnRepository _switchedOnRepository;
        private readonly ITakeOffRepository _takeOffRepository;
        private readonly IClimbRepository _climbRepository;
        private readonly ICruiseRepository _cruiseRepository;
        private readonly IDeclineRepository _declineRepository;
        private readonly ICrewRepository _crewRepository;
        private readonly ISendEmailService _sendEmailService;

        public OperationService(
            DbContextOptions<DBContext> context,
            IOperationsRepository operationsRepository,
            ISwitchedOnRepository switchedOnRepository,
            ITakeOffRepository takeOffRepository,
            IClimbRepository climbRepository,
            ICruiseRepository cruiseRepository,
            IDeclineRepository declineRepository,
            ICrewRepository crewRepository,
            ISendEmailService sendEmailService
        )
        {
            _contextOptions = context;
            _operationsRepository = operationsRepository;
            _switchedOnRepository = switchedOnRepository;
            _takeOffRepository = takeOffRepository;
            _climbRepository = climbRepository;
            _cruiseRepository = cruiseRepository;
            _declineRepository = declineRepository;
            _crewRepository = crewRepository;
            _sendEmailService = sendEmailService;
        }

        public async Task<List<OperationResponse>> GetOperations()
        {
            try{
                var response = await _operationsRepository.GetOperations();
                return response;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<List<OperationResponse>> GetOperationsFilters(string routes)
        {
            try{
                var response = await _operationsRepository.GetOperationsFilters(routes);
                return response;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<List<OperationResponse>> GetOperationsFilterByDateTime(DateTime date)
        {
            try{
                var response = await _operationsRepository.GetOperationsFilterByDateTime(date);
                return response;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<List<OperationsResponse>> GetOperation(int id)
        {
            try{
                var operation = await _operationsRepository.GetOperation(id);

                foreach (var item in operation)
                {
                    var switchedOnResponse = await _switchedOnRepository.GetSwitchedOnByOperation(item.Id);
                    item.SwitchedOnList = switchedOnResponse;

                    var takeoffResponse = await _takeOffRepository.GetTakeOffByOperation(item.Id);
                    item.TakeOffList = takeoffResponse;

                    var climbResponse = await _climbRepository.GetClimbByOperation(item.Id);
                    item.ClimbList = climbResponse;

                    var cruiseResponse = await _cruiseRepository.GetCruiseByOperation(item.Id);
                    item.CruiseList = cruiseResponse;

                    var declineResponse = await _declineRepository.GetDeclineByOperation(item.Id);
                    item.DeclineList = declineResponse;

                    var crewResponse = await _crewRepository.GetCrewByOperation(item.Id);
                    item.CrewList = crewResponse;
                }
                return operation;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<List<OperationsResponse>> GetOperationsByRoutes(string routeFrom, string routeTo)
        {
            try{
                var operation = await _operationsRepository.GetOperationsByRoutes(routeFrom, routeTo);

                foreach (var item in operation)
                {
                    var switchedOnResponse = await _switchedOnRepository.GetSwitchedOnByOperation(item.Id);
                    item.SwitchedOnList = switchedOnResponse;

                    var takeoffResponse = await _takeOffRepository.GetTakeOffByOperation(item.Id);
                    item.TakeOffList = takeoffResponse;

                    var climbResponse = await _climbRepository.GetClimbByOperation(item.Id);
                    item.ClimbList = climbResponse;

                    var cruiseResponse = await _cruiseRepository.GetCruiseByOperation(item.Id);
                    item.CruiseList = cruiseResponse;

                    var declineResponse = await _declineRepository.GetDeclineByOperation(item.Id);
                    item.DeclineList = declineResponse;

                    var crewResponse = await _crewRepository.GetCrewByOperation(item.Id);
                    item.CrewList = crewResponse;
                }
                return operation;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<Operation> PostOperation(OperationPayload operationPayload)
        {
            try{
                var operationCreated = await _operationsRepository.PostOperation(operationPayload);
                if(operationCreated.Id != 0){
                    operationPayload.SwitchedOnPayload.OperationId = operationCreated.Id;
                    var switchedCreated = await _switchedOnRepository.PostSwitchedOn(operationPayload.SwitchedOnPayload);

                    if(switchedCreated.Id != 0)
                        operationPayload.TakeOffPayload.OperationId = operationCreated.Id;
                        var takeoffCreated = await _takeOffRepository.PostTakeOff(operationPayload.TakeOffPayload);

                    if(takeoffCreated.Id != 0)
                        operationPayload.ClimbPayload.OperationId = operationCreated.Id;
                        var climbCreated = await _climbRepository.PostClimb(operationPayload.ClimbPayload);

                    if(climbCreated.Id != 0)
                        operationPayload.CruisePayload.OperationId = operationCreated.Id;
                        var cruiseCreated = await _cruiseRepository.PostCruise(operationPayload.CruisePayload);

                    if(cruiseCreated.Id != 0)
                        operationPayload.DeclinePayload.OperationId = operationCreated.Id;
                        var declineCreated = await _declineRepository.PostDecline(operationPayload.DeclinePayload);

                    if(declineCreated.Id != 0)
                        operationPayload.CrewPayload.OperationId = operationCreated.Id;
                        await _crewRepository.PostCrew(operationPayload.CrewPayload);
                }
                return operationCreated;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<Operation> PutOperation(int id, Operation operation)
        {
            try{
                var response = await _operationsRepository.PutOperation(id, operation);
                return response;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<Operation> DeleteOperation(int id)
        {
            try{
                var response = await _operationsRepository.DeleteOperation(id);
                return response;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public void SendEmail(int id, string jsonData, OperationPayload operations)
        {
            byte[] jsonByteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(jsonData);

            _sendEmailService.Send(new Email
            {
                Template = "format.html",
                Title = "Operación creada",
                Subject = "Operación " + id + " Ruta " + operations.RouteFrom + " " + operations.RouteTo,
                TO = operations.UserEmail,
                withCCO = true,
                fileName = operations.RouteFrom + "_" + operations.RouteTo + "_" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"),
                file = jsonByteArray
            });
        }
    }
}
