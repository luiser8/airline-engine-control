using EngineMonitoring.Models;
using EngineMonitoring.Request.Cruise.Payloads;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Repository
{
    public class CruiseRepository : ICruiseRepository
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        public CruiseRepository(DbContextOptions<DBContext> context)
        {
            _contextOptions = context;
        }

        public async Task<List<CruiseResponse>> GetCruiseByOperation(int operationId)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.Cruises
                            .Where(s => s.OperationId == operationId)
                            .ToListAsync();

                    var engineDetails = await _context.EngineDetails
                        .Where(ed => ed.OperationId == operationId && ed.EngineEventId == response[0].EngineEventId)
                        .ToListAsync();

                    var detailsInfo = await _context.DetailsInfos
                        .Where(di => di.Id == response[0].DetailsInfoId)
                        .ToListAsync();

                    var cruises = new List<CruiseResponse>();
                    var cruiseEngineDetail = new List<EngineDetailResponse>();
                    var cruiseDetailInfo = new List<DetailsInfoResponse>();

                    foreach (var item in detailsInfo)
                    {
                        cruiseDetailInfo.Add(new DetailsInfoResponse
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
                        cruiseEngineDetail.Add(new EngineDetailResponse
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
                        cruises.Add(new CruiseResponse
                        {
                            DetailsInfo = cruiseDetailInfo,
                            EngineDetailList = cruiseEngineDetail
                        });
                    }

                    return cruises;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<Cruise> PostCruise(CruisePayload cruisePayload)
        {
            try
            {
                using (var _context = new DBContext(_contextOptions))
                {
                    var detailsInfo = new DetailsInfo
                    {
                        Tat = cruisePayload.DetailsInfoPayload.TAT,
                        Mach = cruisePayload.DetailsInfoPayload.Mach,
                        Paltitude = cruisePayload.DetailsInfoPayload.PAltitude,
                        PackValve1 = cruisePayload.DetailsInfoPayload.PackValve1,
                        PackValve2 = cruisePayload.DetailsInfoPayload.PackValve2,
                        ConfAton = cruisePayload.DetailsInfoPayload.ConfATon,
                        IsolationValve = cruisePayload.DetailsInfoPayload.IsolationValve,
                        WingAi = cruisePayload.DetailsInfoPayload.WingAI,
                        ReducedPower = cruisePayload.DetailsInfoPayload.ReducedPower,
                        RegularPower = cruisePayload.DetailsInfoPayload.RegularPower,
                        ConfAtoff = cruisePayload.DetailsInfoPayload.ConfAToff
                    };

                    var detailsInfoSaved = await _context.DetailsInfos.AddAsync(detailsInfo);
                    await _context.SaveChangesAsync();

                    var cruise = new Cruise
                    {
                        OperationId = cruisePayload.OperationId,
                        EngineEventId = cruisePayload.EngineEventId,
                        DetailsInfoId = detailsInfoSaved.Entity.Id
                    };
                    var cruiseSaved = await _context.Cruises.AddAsync(cruise);
                    await _context.SaveChangesAsync();

                    foreach (var ed in cruisePayload.EngineDetailPayload)
                    {
                        var engineDetail = new EngineDetail
                        {
                            OperationId = cruisePayload.OperationId,
                            EngineEventId = cruisePayload.EngineEventId,
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
                    return cruise;
                }
            }
            catch (DbUpdateException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}