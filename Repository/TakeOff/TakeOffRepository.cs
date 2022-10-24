using EngineMonitoring.Models;
using EngineMonitoring.Request.TakeOff.Payloads;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Repository
{
    public class TakeOffRepository : ITakeOffRepository
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        public TakeOffRepository(DbContextOptions<DBContext> context)
        {
            _contextOptions = context;
        }

        public async Task<List<TakeOffResponse>> GetTakeOffByOperation(int operationId)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.TakeOffs
                            .Where(s => s.OperationId == operationId)
                            .ToListAsync();

                    var engineDetails = await _context.EngineDetails
                        .Where(ed => ed.OperationId == operationId && ed.EngineEventId == response[0].EngineEventId)
                        .ToListAsync();

                    var detailsInfo = await _context.DetailsInfos
                        .Where(di => di.Id == response[0].DetailsInfoId)
                        .ToListAsync();

                    var takeOff = new List<TakeOffResponse>();
                    var takeoffEngineDetail = new List<EngineDetailResponse>();
                    var takeOffDetailInfo = new List<DetailsInfoResponse>();

                    foreach (var item in detailsInfo)
                    {
                        takeOffDetailInfo.Add(new DetailsInfoResponse
                        {
                            Tat = item.Tat,
                            Mach = item.Mach,
                            Paltitude = item.Paltitude,
                            PackValve1 = item.PackValve1,
                            PackValve2 = item.PackValve2,
                            ConfAton = item.ConfAton,
                            ConfAtoff = item.ConfAtoff,
                            IsolationValve = item.IsolationValve,
                            WingAi = item.WingAi,
                            ReducedPower = item.ReducedPower,
                            RegularPower = item.RegularPower
                        });
                    }

                    foreach (var item in engineDetails)
                    {
                        takeoffEngineDetail.Add(new EngineDetailResponse
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
                        takeOff.Add(new TakeOffResponse
                        {
                            DetailsInfo = takeOffDetailInfo,
                            EngineDetailList = takeoffEngineDetail
                        });
                    }

                    return takeOff;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<TakeOff> PostTakeOff(TakeOffPayload takeOffPayload)
        {
            try
            {
                using (var _context = new DBContext(_contextOptions))
                {
                    var detailsInfo = new DetailsInfo
                    {
                        Tat = takeOffPayload.DetailsInfoPayload.TAT,
                        Mach = takeOffPayload.DetailsInfoPayload.Mach,
                        Paltitude = takeOffPayload.DetailsInfoPayload.PAltitude,
                        PackValve1 = takeOffPayload.DetailsInfoPayload.PackValve1,
                        PackValve2 = takeOffPayload.DetailsInfoPayload.PackValve2,
                        ConfAton = takeOffPayload.DetailsInfoPayload.ConfATon,
                        IsolationValve = takeOffPayload.DetailsInfoPayload.IsolationValve,
                        WingAi = takeOffPayload.DetailsInfoPayload.WingAI,
                        ReducedPower = takeOffPayload.DetailsInfoPayload.ReducedPower,
                        RegularPower = takeOffPayload.DetailsInfoPayload.RegularPower,
                        ConfAtoff = takeOffPayload.DetailsInfoPayload.ConfAToff
                    };

                    var detailsInfoSaved = await _context.DetailsInfos.AddAsync(detailsInfo);
                    await _context.SaveChangesAsync();

                    var takeOff = new TakeOff
                    {
                        OperationId = takeOffPayload.OperationId,
                        EngineEventId = takeOffPayload.EngineEventId,
                        DetailsInfoId = detailsInfoSaved.Entity.Id
                    };
                    var takeOffSaved = await _context.TakeOffs.AddAsync(takeOff);
                    await _context.SaveChangesAsync();

                    foreach (var ed in takeOffPayload.EngineDetailPayload)
                    {
                        var engineDetail = new EngineDetail
                        {
                            OperationId = takeOffPayload.OperationId,
                            EngineEventId = takeOffPayload.EngineEventId,
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
                    return takeOff;
                }
            }
            catch (DbUpdateException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}