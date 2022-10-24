using EngineMonitoring.Models;
using EngineMonitoring.Request.Crew.Payloads;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Repository
{
    public class CrewRepository : ICrewRepository
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        public CrewRepository(DbContextOptions<DBContext> context)
        {
            _contextOptions = context;
        }

        public async Task<List<CrewResponse>> GetCrewByOperation(int operationId)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.Crews
                            .Where(s => s.OperationId == operationId)
                            .ToListAsync();

                    var crew = new List<CrewResponse>();

                    foreach (var item in response)
                    {
                        crew.Add(new CrewResponse
                        {
                            Captain = item.Captain,
                            Fo = item.Fo,
                            Technical = item.Technical
                        });
                    }

                    return crew;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<Crew> PostCrew(CrewPayload crewPayload)
        {
            try
            {
                using (var _context = new DBContext(_contextOptions))
                {
                    var crew = new Crew
                    {
                        OperationId = crewPayload.OperationId,
                        Captain = crewPayload.Captain,
                        Fo = crewPayload.Fo,
                        Technical = crewPayload.Technical
                    };
                    var crewSaved = await _context.Crews.AddAsync(crew);
                    await _context.SaveChangesAsync();

                    return crew;
                }
            }
            catch (DbUpdateException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}