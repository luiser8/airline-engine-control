using EngineMonitoring.Models;
using EngineMonitoring.Request.Operations.Payloads;
using Microsoft.AspNetCore.Mvc;
using EngineMonitoring.Responses;
using EngineMonitoring.Services;
using Newtonsoft.Json;

namespace EngineMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _operationService;
        public OperationsController(
            IOperationService operationService
        ){
            _operationService = operationService;
        }

        /// <summary>Lista operaciones</summary>
        /// <remarks>Es posible listar operaciones creadas.</remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OperationResponse>> GetOperations()
        {
            try
            {
                var response = await _operationService.GetOperations();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>Lista operaciones filtradas por rutas</summary>
        /// <remarks>Es posible listar por filtros de rutas en operaciones creadas.</remarks>
        /// <param name="routes">Valor para ubicar operaciones.</param>
        [HttpGet]
        [Route("route/{route}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OperationResponse>> GetOperationsFilters(string route)
        {
            try
            {
                var response = await _operationService.GetOperationsFilters(route);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>Lista operaciones filtradas por fechas de creación</summary>
        /// <remarks>Es posible listar por filtros por fechas de creación en operaciones creadas.</remarks>
        /// <param name="date">Valor para ubicar operaciones.</param>
        [HttpGet]
        [Route("date/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OperationResponse>> GetOperationsFilterByDateTime(DateTime date)
        {
            try
            {
                var response = await _operationService.GetOperationsFilterByDateTime(date);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>Lista operaciones por el ID unico</summary>
        /// <remarks>Es posible listar una operacion creada.</remarks>
        /// <param name="id">Identificador unico de una operacion.</param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OperationsResponse>> GetOperation(int id)
        {
            try
            {
                var response = await _operationService.GetOperation(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>Lista operaciones</summary>
        /// <remarks>Es posible listar operaciones creadas.</remarks>
        /// <param name="routeFrom">Ruta de origen del vuelo correspondiente a una operacion.</param>
        /// <param name="routeTo">Ruta de destino del vuelo correspondiente a una operacion.</param>
        [HttpGet]
        [Route("{routeFrom}/{routeTo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OperationsResponse>> GetOperationsByRoutes(string routeFrom, string routeTo)
        {
            try
            {
                var response = await _operationService.GetOperationsByRoutes(routeFrom, routeTo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>Crea operaciones</summary>
        /// <remarks>Es posible crear operaciones.</remarks>
        /// <param name="operationPayload">Parámetros para crear una operacion.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Operation>> CreateOperation([FromBody] OperationPayload operationPayload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _operationService.PostOperation(operationPayload);

                if (result.Id != 0 || result != null)
                {
                    var operation = await _operationService.GetOperation(result.Id);

                    string jsonData = JsonConvert.SerializeObject(operation, Formatting.Indented);

                    _operationService.SendEmail(result.Id, jsonData, operationPayload);
                }
                return Created("Create", result.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>Actualiza una operacion</summary>
        /// <remarks>Es posible actualizar operaciones.</remarks>
        /// <param name="id">Identificador unico de una operacion.</param>
        /// <param name="operation">Parámetros para actualizar una operacion.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Operation>> PutOperation(int id, [FromBody] Operation operation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var request = await _operationService.PutOperation(id, operation);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>Elimina una operacion</summary>
        /// <remarks>Es posible eliminar operaciones.</remarks>
        /// <param name="id">Identificador único de una operacion.</param>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Operation>> DeleteOperation(int id)
        {
            if (id == 0)
            {
                return BadRequest("Debes colocar ID de operacion a eliminar");
            }
            try
            {
                var request = await _operationService.DeleteOperation(id);
                return Ok(request.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}