using EngineMonitoring.Models;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Repository
{
    public class EngineEventRepository : IEngineEventRepository
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        public EngineEventRepository(DbContextOptions<DBContext> context)
        {
            _contextOptions = context;
        }

        public async Task<List<EngineEventResponse>> GetEngineEvents()
        {
            try{
            using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.EngineEvents.ToListAsync();
                    var engineEvents = new List<EngineEventResponse>();
                    foreach(var item in response)
                    {
                        engineEvents.Add(new EngineEventResponse
                        {
                            Id = item.Id,
                            EventName = item.EventName,
                            ForEvent = item.ForEvent
                        });
                    }
                    return engineEvents;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<EngineEventResponse> GetEngineEvent(int forEvent)
        {
            try{
            using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.EngineEvents.SingleOrDefaultAsync(o => o.ForEvent == forEvent);
                    var engineEvent = new EngineEventResponse {
                        Id = response.Id,
                        EventName = response.EventName,
                        ForEvent = response.ForEvent
                    };
                    return engineEvent;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<List<EngineDetailResponse>> GetEngineDetails(int operationId, int engineEventId)
        {
            try{
            using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.EngineDetails
                        .Where(ed => ed.OperationId == operationId && ed.EngineEventId == engineEventId)
                        .ToListAsync();

                    var engineDetails = new List<EngineDetailResponse>();
                    foreach(var item in response)
                    {
                        engineDetails.Add(new EngineDetailResponse {});
                    }
                    return engineDetails;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}