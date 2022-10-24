using EngineMonitoring.Services;
using EngineMonitoring.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EngineMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineEventController : ControllerBase
    {
        private readonly IEngineEventService _engineEventService;
        public EngineEventController(IEngineEventService engineEventService)
        {
            _engineEventService = engineEventService;
        }

        /// <summary>Lista de eventos de motores</summary>
        /// <remarks>Es posible listar eventos de motores.</remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EngineEventResponse>> GetEngineEvents()
        {
            try
            {
                var response = await _engineEventService.GetEngineEvents();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>Evento de motores por el evento unico</summary>
        /// <remarks>Es posible listar eventos de un motor.</remarks>
        /// <param name="forEvent" example="1">Identificador único de un evento</param>
        [HttpGet]
        [Route("{forEvent}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EngineEventResponse>> GetEngineEvent(int forEvent)
        {
            try
            {
                var response = await _engineEventService.GetEngineEvent(forEvent);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}