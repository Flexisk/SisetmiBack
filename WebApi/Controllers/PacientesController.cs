using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dominio.Pacientes;
using System.Net;
using Aplicacion.Services;
using Aplicacion.ManejadorErrores;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IGenericService<Pacientes> _service;
        public PacientesController(IGenericService<Pacientes> service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pacientes>>> GetPaciente()
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NoContent, "No Existe", "No se encontraron Pacientes en Base de Datos");
            }
            return await _service.GetAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Pacientes>> GetPaciente(long Id)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron Pacientes con este Id");
            }
            var pacientes = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");
            if (pacientes.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron Pacientes con este Id");
            }
            return Created("ObtenerPaciente", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se obtuvo Id Solicitado", pacientes = pacientes });
        }

        [HttpPost]
        public async Task<ActionResult<Pacientes>> PostPaciente(Pacientes pacientes)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Modificado", "No fue posible Modificar Paciente");
            }

            await _service.CreateAsync(pacientes);

            return Created("CrearPaciente", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Creo el Paciente con Exito", pacientes = pacientes });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacientes(long Id, Pacientes pacientes)
        {
            if (Id != pacientes.Id)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No fue posible encontrar el Id del Paciente");
            }

            bool updated = await _service.UpdateAsync(Id, pacientes);

            if (!updated)
            {
                throw new ExcepcionError(HttpStatusCode.NotModified, "No Modificado", "No se pudo Actualizar el Paciente");
            }
            return Created("ActualizarPaciente", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Actualizo con Exito el Paciente" });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacientes(long Id)
        {
            var pacientes = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (pacientes.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron Pacientes con este Id");
            }
            await _service.DeleteAsync(Id);
            return Created("EliminarPacientes", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Elimino con Exito el Paciente" });
        }
    }
}
