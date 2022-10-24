using EngineMonitoring.Models;
using EngineMonitoring.Request.Decline.Payloads;
using EngineMonitoring.Responses;
using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Repository
{
    public class DeclineRepository : IDeclineRepository
    {
        private readonly DbContextOptions<DBContext> _contextOptions;
        public DeclineRepository(DbContextOptions<DBContext> context)
        {
            _contextOptions = context;
        }

        public async Task<List<DeclineResponse>> GetDeclineByOperation(int operationId)
        {
            try{
                using (var _context = new DBContext(_contextOptions))
                {
                    var response = await _context.Declines
                            .Where(s => s.OperationId == operationId)
                            .ToListAsync();

                    var engineDetails = await _context.EngineDetails
                        .Where(ed => ed.OperationId == operationId && ed.EngineEventId == response[0].EngineEventId)
                        .ToListAsync();

                    var detailsInfo = await _context.DetailsInfos
                        .Where(di => di.Id == response[0].DetailsInfoId)
                        .ToListAsync();

                    var decline = new List<DeclineResponse>();
                    var declineEngineDetail = new List<EngineDetailResponse>();
                    var declineDetailInfo = new List<DetailsInfoResponse>();

                    foreach (var item in detailsInfo)
                    {
                        declineDetailInfo.Add(new DetailsInfoResponse
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
                        declineEngineDetail.Add(new EngineDetailResponse
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
                        decline.Add(new DeclineResponse
                        {
                            DetailsInfo = declineDetailInfo,
                            EngineDetailList = declineEngineDetail
                        });
                    }

                    return decline;
                }
            }catch(Exception ex){
                throw new NotImplementedException(ex.Message);
            }
        }
        public async Task<Decline> PostDecline(DeclinePayload declinePayload)
        {
            try
            {
                using (var _context = new DBContext(_contextOptions))
                {
                    var detailsInfo = new DetailsInfo
                    {
                        Tat = declinePayload.DetailsInfoPayload.TAT,
                        Mach = declinePayload.DetailsInfoPayload.Mach,
                        Paltitude = declinePayload.DetailsInfoPayload.PAltitude,
                        PackValve1 = declinePayload.DetailsInfoPayload.PackValve1,
                        PackValve2 = declinePayload.DetailsInfoPayload.PackValve2,
                        ConfAton = declinePayload.DetailsInfoPayload.ConfATon,
                        IsolationValve = declinePayload.DetailsInfoPayload.IsolationValve,
                        WingAi = declinePayload.DetailsInfoPayload.WingAI,
                        ReducedPower = declinePayload.DetailsInfoPayload.ReducedPower,
                        RegularPower = declinePayload.DetailsInfoPayload.RegularPower,
                        ConfAtoff = declinePayload.DetailsInfoPayload.ConfAToff
                    };

                    var detailsInfoSaved = await _context.DetailsInfos.AddAsync(detailsInfo);
                    await _context.SaveChangesAsync();

                    var decline = new Decline
                    {
                        OperationId = declinePayload.OperationId,
                        EngineEventId = declinePayload.EngineEventId,
                        DetailsInfoId = detailsInfoSaved.Entity.Id
                    };
                    var declineSaved = await _context.Declines.AddAsync(decline);
                    await _context.SaveChangesAsync();

                    foreach (var ed in declinePayload.EngineDetailPayload)
                    {
                        var engineDetail = new EngineDetail
                        {
                            OperationId = declinePayload.OperationId,
                            EngineEventId = declinePayload.EngineEventId,
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
                    return decline;
                }
            }
            catch (DbUpdateException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}