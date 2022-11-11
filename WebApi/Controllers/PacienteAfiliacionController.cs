using Aplicacion.ManejadorErrores;
using Aplicacion.Services;
using Dominio.Pacientes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteAfiliacionController : ControllerBase
    {
        private readonly IGenericService<PacienteAfiliacion> _service;
        public PacienteAfiliacionController(IGenericService<PacienteAfiliacion> service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteAfiliacion>>> GetPacienteAfiliacion()
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NoContent, "No Existe", "No se encontraron PacientesAfiliacion en Base de Datos");
            }
            return await _service.GetAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteAfiliacion>> GetPacienteAfiliacion(long Id)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacientesAfiliacion con este Id");
            }
            var pacientesAfiliacion = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");
            if (pacientesAfiliacion.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacientesAfiliacion con este Id");
            }
            return Created("ObtenerPacientesAfiliacion", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se obtuvo Id Solicitado", pacientesAfiliacion = pacientesAfiliacion });
        }

        [HttpPost]
        public async Task<ActionResult<PacienteAfiliacion>> PostPacienteAfiliacion(PacienteAfiliacion pacienteAfiliacion)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Modificado", "No fue posible Modificar PacienteAfiliacion");
            }
            await _service.CreateAsync(pacienteAfiliacion);
            return Created("CrearUsuario", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Creo el PacienteAfiliacion con Exito" });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteAfiliacion(long Id, PacienteAfiliacion pacienteAfiliacion)
        {
            if (Id != pacienteAfiliacion.Id)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No fue posible encontrar el Id del PacienteAfiliacion");
            }

            bool updated = await _service.UpdateAsync(Id, pacienteAfiliacion);

            if (!updated)
            {
                throw new ExcepcionError(HttpStatusCode.NotModified, "No Modificado", "No se pudo Actualizar el PacienteAfiliacion");
            }
            return Created("ActualizarPacienteAfiliacion", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Actualizo con Exito el PacienteAfiliacion" });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacienteAfiliacion(long Id)
        {
            var pacienteAfiliacion = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (pacienteAfiliacion.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteAfiliacion con este Id");
            }
            await _service.DeleteAsync(Id);
            return Created("EliminarPacienteAfiliacion", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Elimino con Exito el PacienteAfiliacion" });
        }

    }
}
