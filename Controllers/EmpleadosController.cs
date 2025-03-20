using Crud.DTO_s;
using Crud.Models;
using Crud.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{

    [Route("api/Empleados")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosServices _IEmpleado;
        private readonly ILogger<EmpleadosController> _logger;


        /// Cuando registras el servicio en el contenedor de 
        /// dependencias, le dices al contenedor que siempre 
        /// que alguien solicite IUsuarioService, 
        /// debe proporcionar una instancia de UsuarioService


        public EmpleadosController(IEmpleadosServices IEmpleado, ILogger<EmpleadosController> logger)
        {
            _IEmpleado = IEmpleado;//obtiene instancia de EmpleadoServices
            _logger = logger;
        }

        // GET: api/Empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoDTO>>> GetEmpleados()
        {

            //NotFound() método que crea una respuesta HTTP 404 Not Found.
            //condición ? resultado_si_verdadero : resultado_si_falso
            try
            {
                var empleados = await _IEmpleado.GetEmpleados();

                return empleados == null ? NotFound("no se encontró ningun empleado") : Ok(empleados);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo establecer conexion con la base de datos");
                return StatusCode(500, "Ocurrió un error al intentar obtener los empleados. Por favor, inténtelo de nuevo más tarde.");

            }


        }


        // GET: api/Empleados/2
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoDTO>> GetEmpleado(int id)
        {
            try
            {
                Console.WriteLine("->>>>>>>>>>>><");
                var empleado = await _IEmpleado.GetEmpleadoById(id);
                if (empleado == null)
                {
                    return NotFound($"no se pudo encontrar el empleado con id: {id}");
                }
                return Ok(empleado);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el empleado con ID: {Id}", id);
                return StatusCode(500, "Ocurrio un error al intentar buscar el empleado.");
            }

        }

        // GET: api/Empleados/2/
        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> EmpleadoExists(int id)
        {
            try
            {
                var aux = await _IEmpleado.EmpleadoExists(id);
                return aux == false ? NotFound() : Ok(aux);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "no se puede validar la exixtencia del empleado {id}", id);
                return NotFound();
            }


        }






        //Al recibir una solicitud HTTP POST, el atributo [FromBody]
        //toma el contenido del cuerpo de la solicitud (que generalmente es JSON)
        //y lo convierte en un objeto del tipo especificado (Empleado en este caso).


        [HttpPost]
        public async Task<ActionResult<EmpleadoDTO>> AddEmpleado([FromBody] CrearEmpleadoDTO emp)
        {

            try
            {
                _logger.LogInformation("VA A ENTRAR A ADDeMPLEADO DESDE EL CONTROLADOOOOOOOOOOOOOOOOOOOOOOOOR");
                var nuevoEmpleado = await _IEmpleado.AddEmpleado(emp);
                return nuevoEmpleado == null ? BadRequest("Faltan datos del empleado.") : Ok(nuevoEmpleado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar ingresar el empleado.");
                return StatusCode(500, "Ocurrió un error al intentar ingresar el empleado. Por favor, inténtelo de nuevo más tarde.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmpleadoDTO>> DeleteEmpleado(int id)
        {
            try
            {
                var empleadoEliminado = await _IEmpleado.EliminarEmpleado(id);

                return empleadoEliminado == null ? BadRequest("No se encontró el empleado") : Ok(empleadoEliminado);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar eliminar el empleado");
                return StatusCode(500, "Ocurrió un error al intentar eliminar el empleado. Por favor, inténtelo de nuevo más tarde.");

            }
        }



        [HttpPatch("{id}")]
        //probarlo
       // PATCH se utiliza para realizar actualizaciones parciales a una entidad.

        public async Task<ActionResult<EmpleadoDTO>> ActualizarEmpleado(int id, [FromBody] JsonPatchDocument<EmpleadoDTO> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    _logger.LogWarning("El documento de parche es nulo.");
                    return BadRequest("Documento de parche inválido");
                }

                _logger.LogInformation("Llamando al servicio para actualizar el empleado con ID: {Id}", id);
                var empActualizado = await _IEmpleado.ActualizarEmpleado(id, patchDocument);

                return (empActualizado == null
                    ? BadRequest("No se pudo actualizar el empleado, problemas al acceder a la base de datos")
                    : Ok(empActualizado));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al intentar actualizar el empleado, inténtelo de nuevo más tarde");
                return StatusCode(500, "Ocurrió un error al intentar actualizar el empleado. Por favor, inténtelo de nuevo más tarde.");
            }
        }


        //el metodo HttpPut actualiza TODA la Entidad empleadoDTO
        [HttpPut("{id}")]
        public async Task<ActionResult<EmpleadoDTO>> ActualizarEmpleado(int id, [FromBody] EmpleadoDTO empleado)
        {
            if (empleado == null)
            {
                _logger.LogWarning("la variable 'empleado' se encuentra vacía");
                return BadRequest("la variable 'empleado' debe contener el tipo de dato 'Empleado'");
            }
            try
            {
                var empActualizado = await _IEmpleado.ActualizarEmpleadoConPut(id, empleado);
                return (empActualizado == null ? BadRequest("no se pudo actualizar el empleado") : Ok(empActualizado));
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al intentar actualizar el empleado, inténtelo de nuevo más tarde");
                return StatusCode(500, "Ocurrió un error al intentar actualizar el empleado. Por favor, inténtelo de nuevo más tarde.");
            }
        }




    }
}
