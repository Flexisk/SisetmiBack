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
    public class PacienteDiagnosticoController : ControllerBase
    {
        private readonly IGenericService<PacienteDiagnostico> _service;
        public PacienteDiagnosticoController(IGenericService<PacienteDiagnostico> service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDiagnostico>>> GetPacienteDiagnostico()
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NoContent, "No Existe", "No se encontraron PacienteDiagnostico en Base de Datos");
            }
            return await _service.GetAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteDiagnostico>> GetPacienteDiagnostico(long Id)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteDiagnostico con este Id");
            }
            var pacienteDiagnostico = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");
            if (pacienteDiagnostico.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteDiagnostico con este Id");
            }
            return Created("ObtenerPacienteDiagnostico", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se obtuvo Id Solicitado", pacienteDiagnostico = pacienteDiagnostico });
        }

        [HttpPost]
        public async Task<ActionResult<PacienteDiagnostico>> PostPacienteDiagnostico(PacienteDiagnostico pacienteDiagnostico)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Modificado", "No fue posible Modificar PacienteDiagnostico");
            }

            await _service.CreateAsync(pacienteDiagnostico);

            return Created("CrearPaciente", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Creo el Paciente con Exito", pacienteDiagnostico = pacienteDiagnostico });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteDiagnostico(long Id, PacienteDiagnostico pacienteDiagnostico)
        {
            if (Id != pacienteDiagnostico.Id)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No fue posible encontrar el Id del PacienteDiagnostico");
            }

            bool updated = await _service.UpdateAsync(Id, pacienteDiagnostico);

            if (!updated)
            {
                throw new ExcepcionError(HttpStatusCode.NotModified, "No Modificado", "No se pudo Actualizar el PacienteDiagnostico");
            }
            return Created("ActualizarPacienteDiagnostico", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Actualizo con Exito el PacienteDiagnostico" });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacienteDiagnostico(long Id)
        {
            var pacientes = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (pacientes.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteDiagnostico con este Id");
            }
            await _service.DeleteAsync(Id);
            return Created("EliminarPacienteDiagnostico", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Elimino con Exito el PacienteDiagnostico" });
        }
    }
}
