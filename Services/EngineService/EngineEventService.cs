using EngineMonitoring.Repository;
using EngineMonitoring.Models;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Services
{
    public class EngineEventService : IEngineEventService
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        private readonly IEngineEventRepository _engineEventRepository;
        public EngineEventService(DbContextOptions<DBContext> context, IEngineEventRepository engineEventRepository)
        {
            _contextOptions = context;
            _engineEventRepository = engineEventRepository;
        }

        public async Task<List<EngineEventResponse>> GetEngineEvents()
        {
            try{
                var response = await _engineEventRepository.GetEngineEvents();
                return response;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<EngineEventResponse> GetEngineEvent(int forEvent)
        {
            try{
                var response = await _engineEventRepository.GetEngineEvent(forEvent);
                return response;
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}