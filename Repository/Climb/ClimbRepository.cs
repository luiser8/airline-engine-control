using EngineMonitoring.Models;
using EngineMonitoring.Request.Climb.Payloads;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Repository
{
    public class ClimbRepository : IClimbRepository
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        public ClimbRepository(DbContextOptions<DBContext> context)
        {
            _contextOptions = context;
        }

        public async Task<List<ClimbResponse>> GetClimbByOperation(int operationId)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.Climbs
                            .Where(s => s.OperationId == operationId)
                            .ToListAsync();

                    var engineDetails = await _context.EngineDetails
                        .Where(ed => ed.OperationId == operationId && ed.EngineEventId == response[0].EngineEventId)
                        .ToListAsync();

                    var detailsInfo = await _context.DetailsInfos
                        .Where(di => di.Id == response[0].DetailsInfoId)
                        .ToListAsync();

                    var climbs = new List<ClimbResponse>();
                    var climbEngineDetail = new List<EngineDetailResponse>();
                    var climbDetailInfo = new List<DetailsInfoResponse>();

                    foreach (var item in detailsInfo)
                    {
                        climbDetailInfo.Add(new DetailsInfoResponse
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
                        climbEngineDetail.Add(new EngineDetailResponse
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
                        climbs.Add(new ClimbResponse
                        {
                            DetailsInfo = climbDetailInfo,
                            EngineDetailList = climbEngineDetail
                        });
                    }

                    return climbs;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<Climb> PostClimb(ClimbPayload climbPayload)
        {
            try
            {
                using (var _context = new DBContext(_contextOptions))
                {
                    var detailsInfo = new DetailsInfo
                    {
                        Tat = climbPayload.DetailsInfoPayload.TAT,
                        Mach = climbPayload.DetailsInfoPayload.Mach,
                        Paltitude = climbPayload.DetailsInfoPayload.PAltitude,
                        PackValve1 = climbPayload.DetailsInfoPayload.PackValve1,
                        PackValve2 = climbPayload.DetailsInfoPayload.PackValve2,
                        ConfAton = climbPayload.DetailsInfoPayload.ConfATon,
                        IsolationValve = climbPayload.DetailsInfoPayload.IsolationValve,
                        WingAi = climbPayload.DetailsInfoPayload.WingAI,
                        ReducedPower = climbPayload.DetailsInfoPayload.ReducedPower,
                        RegularPower = climbPayload.DetailsInfoPayload.RegularPower,
                        ConfAtoff = climbPayload.DetailsInfoPayload.ConfAToff
                    };

                    var detailsInfoSaved = await _context.DetailsInfos.AddAsync(detailsInfo);
                    await _context.SaveChangesAsync();

                    var climb = new Climb
                    {
                        OperationId = climbPayload.OperationId,
                        EngineEventId = climbPayload.EngineEventId,
                        DetailsInfoId = detailsInfoSaved.Entity.Id
                    };
                    var climbSaved = await _context.Climbs.AddAsync(climb);
                    await _context.SaveChangesAsync();

                    foreach (var ed in climbPayload.EngineDetailPayload)
                    {
                        var engineDetail = new EngineDetail
                        {
                            OperationId = climbPayload.OperationId,
                            EngineEventId = climbPayload.EngineEventId,
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
                    return climb;
                }
            }
            catch (DbUpdateException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}