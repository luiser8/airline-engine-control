using EngineMonitoring.Models;
using EngineMonitoring.Request.SwitchedOn.Payloads;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Repository
{
    public class SwitchedOnRepository : ISwitchedOnRepository
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        public SwitchedOnRepository(DbContextOptions<DBContext> context)
        {
            _contextOptions = context;
        }

        public async Task<List<SwitchedOnResponse>> GetSwitchedOnByOperation(int operationId)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.SwitchedOns
                            .Where(s => s.OperationId == operationId)
                            .ToListAsync();

                    var engineDetails = await _context.EngineDetails
                        .Where(ed => ed.OperationId == operationId && ed.EngineEventId == response[0].EngineEventId)
                        .ToListAsync();

                    var switchedOn = new List<SwitchedOnResponse>();
                    var switchedOnEngineDetail = new List<EngineDetailResponse>();

                    foreach (var item in engineDetails)
                    {
                        switchedOnEngineDetail.Add(new EngineDetailResponse
                        {
                            EngineNro = item.EngineNro,
                            N1 = item.N1,
                            N2 = item.N2,
                            Egt = item.Egt,
                            Ff = item.Ff,
                            Vib = item.Vib,
                            EngineBleed = item.EngineBleed,
                            InletAi = item.InletAi,
                            OilPressure = item.OilPressure,
                            OilTemp = item.OilTemp
                        });
                    }

                    foreach (var item in response)
                    {
                        switchedOn.Add(new SwitchedOnResponse
                        {
                            EngineDetailList = switchedOnEngineDetail
                        });
                    }

                    return switchedOn;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<SwitchedOn> PostSwitchedOn(SwitchedOnPayload switchedOnPayload)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var switchedOn = new SwitchedOn
                    {
                        OperationId = switchedOnPayload.OperationId,
                        EngineEventId = switchedOnPayload.EngineEventId,
                    };
                    var switchedOnSaved = await _context.SwitchedOns.AddAsync(switchedOn);
                    await _context.SaveChangesAsync();

                    foreach(var ed in switchedOnPayload.EngineDetailPayload)
                    {
                        var engineDetail = new EngineDetail
                        {
                            OperationId = switchedOnPayload.OperationId,
                            EngineEventId = switchedOnPayload.EngineEventId,
                            EngineNro = ed.EngineNro,
                            N1 = ed.N1,
                            N2 = ed.N2,
                            Egt = ed.EGT,
                            Ff = ed.FF,
                            Vib = ed.Vib,
                            EngineBleed = ed.EngineBleed,
                            InletAi = ed.InletAI,
                            OilPressure = ed.OilPressure,
                            OilTemp = ed.OilTemp
                        };

                        var engineDetailSaved = await _context.EngineDetails.AddAsync(engineDetail);
                        await _context.SaveChangesAsync();
                    }

                    return switchedOn;
                }
            }catch(DbUpdateException ex){
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}