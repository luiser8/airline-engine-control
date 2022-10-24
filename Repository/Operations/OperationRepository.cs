using EngineMonitoring.Models;
using EngineMonitoring.Request.Operations.Payloads;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Repository
{
    public class OperationsRepository : IOperationsRepository
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        public OperationsRepository(DbContextOptions<DBContext> context)
        {
            _contextOptions = context;
        }

        public async Task<List<OperationResponse>> GetOperations()
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.Operations.ToListAsync();
                    var operation = new List<OperationResponse>();
                    foreach (var item in response)
                    {
                        operation.Add(new OperationResponse
                        {
                            Id = item.Id,
                            OperationPlotId = item.OperationPlotId,
                            UserEmail = item.UserEmail,
                            RouteFrom = item.RouteFrom,
                            RouteTo = item.RouteTo,
                            Pax = item.Pax,
                            Oat = item.Oat,
                            Pbarometrica = item.Pbarometrica,
                            Personal = item.Personal,
                            Fuel = item.Fuel,
                            Tow = item.Tow,
                            CreationDate = item.CreationDate
                        });
                    }
                    return operation;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<List<OperationsResponse>> GetOperation(int id)
        {
            try{
            using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.Operations.SingleOrDefaultAsync(o => o.Id == id);

                    var operation = new List<OperationsResponse>();
                    string s2 = response.CreationDate.ToString();

                    operation.Add(new OperationsResponse
                    {
                        Id = response.Id,
                        OperationPlotId = response.OperationPlotId,
                        UserEmail = response.UserEmail,
                        RouteFrom = response.RouteFrom,
                        RouteTo = response.RouteTo,
                        Pax = response.Pax,
                        Oat = response.Oat,
                        Pbarometrica = response.Pbarometrica,
                        Personal = response.Personal,
                        Fuel = response.Fuel,
                        Tow = response.Tow,
                        Date = response.CreationDate.ToString("dd/MM/yyyy hh:mm:ss tt")
                    });

                    return operation;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<List<OperationResponse>> GetOperationsFilters(string routes)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.Operations
                    .Where(x => x.RouteFrom.Contains(routes) || x.RouteTo.Contains(routes))
                    .ToListAsync();
                    var operation = new List<OperationResponse>();
                    foreach (var item in response)
                    {
                        operation.Add(new OperationResponse
                        {
                            Id = item.Id,
                            OperationPlotId = item.OperationPlotId,
                            UserEmail = item.UserEmail,
                            RouteFrom = item.RouteFrom,
                            RouteTo = item.RouteTo,
                            Pax = item.Pax,
                            Oat = item.Oat,
                            Pbarometrica = item.Pbarometrica,
                            Personal = item.Personal,
                            Fuel = item.Fuel,
                            Tow = item.Tow,
                            CreationDate = item.CreationDate
                        });
                    }
                    return operation;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<List<OperationResponse>> GetOperationsFilterByDateTime(DateTime date)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.Operations
                    .Where (x=> DateTime.Compare(x.CreationDate.Date, date.Date) == 0)
                    .ToListAsync();
                    var operation = new List<OperationResponse>();
                    foreach (var item in response)
                    {
                        operation.Add(new OperationResponse
                        {
                            Id = item.Id,
                            OperationPlotId = item.OperationPlotId,
                            UserEmail = item.UserEmail,
                            RouteFrom = item.RouteFrom,
                            RouteTo = item.RouteTo,
                            Pax = item.Pax,
                            Oat = item.Oat,
                            Pbarometrica = item.Pbarometrica,
                            Personal = item.Personal,
                            Fuel = item.Fuel,
                            Tow = item.Tow,
                            CreationDate = item.CreationDate
                        });
                    }
                    return operation;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<List<OperationsResponse>> GetOperationsByRoutes(string routeFrom, string routeTo)
        {
            try
            {
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.Operations
                        .Where(o => o.RouteFrom == routeFrom && o.RouteTo == routeTo)
                        .ToListAsync();
                    var operations = new List<OperationsResponse>();

                    foreach (var item in response)
                    {
                        operations.Add(new OperationsResponse
                        {
                            Id = item.Id,
                            OperationPlotId = item.OperationPlotId,
                            UserEmail = item.UserEmail,
                            RouteFrom = item.RouteFrom,
                            RouteTo = item.RouteTo,
                            Pax = item.Pax,
                            Oat = item.Oat,
                            Pbarometrica = item.Pbarometrica,
                            Personal = item.Personal,
                            Fuel = item.Fuel,
                            Tow = item.Tow,
                            Date = item.CreationDate.ToString("dd/MM/yyyy hh:mm:ss tt")
                        });
                    }
                    return operations;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<Operation> PostOperation(OperationPayload operationPayload)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var operation = new Operation
                    {
                        OperationPlotId = operationPayload.OperationPlotId,
                        UserId = operationPayload.UserId,
                        UserEmail = operationPayload.UserEmail,
                        RouteFrom = operationPayload.RouteFrom,
                        RouteTo = operationPayload.RouteTo,
                        Pax = operationPayload.Pax,
                        Oat = operationPayload.OAT,
                        Pbarometrica = operationPayload.Pbarometrica,
                        Personal = operationPayload.Personal,
                        Fuel = operationPayload.Fuel,
                        Tow = operationPayload.TOW
                    };
                    var operationSaved = await _context.Operations.AddAsync(operation);
                    await _context.SaveChangesAsync();

                    return operation;
                }
            }catch(DbUpdateException ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<Operation> PutOperation(int id, Operation operation)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    _context.Entry(operation).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return operation;
                }
            }catch(DbUpdateConcurrencyException ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<Operation> DeleteOperation(int id)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var operation = await _context.Operations.FindAsync(id);
                    if(operation == null)
                    {
                        throw new NotImplementedException("No existe la operacion");
                    }
                    _context.Entry(operation).State = EntityState.Deleted;
                    _context.Operations.Remove(operation);
                    await _context.SaveChangesAsync();
                    return operation;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}