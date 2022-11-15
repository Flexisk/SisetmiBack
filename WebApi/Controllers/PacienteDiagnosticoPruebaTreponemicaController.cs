using Aplicacion.ManejadorErrores;
using Aplicacion.Services;
using Dominio.Paciente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteDiagnosticoPruebaTreponemicaController : ControllerBase
    {
        private readonly IGenericService<PacienteDiagnosticoPruebaTreponemica> _service;
        public PacienteDiagnosticoPruebaTreponemicaController(IGenericService<PacienteDiagnosticoPruebaTreponemica> service)
        {
            this._service = service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDiagnosticoPruebaTreponemica>>> GetPacienteDiagnosticoPruebaTreponemica()
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NoContent, "No Existe", "No se encontraron PacienteDiagnosticoPruebaTreponemica en Base de Datos");
            }
            return await _service.GetAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteDiagnosticoPruebaTreponemica>> GetPacienteDiagnosticoPruebaTreponemica(long Id)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteDiagnosticoPruebaTreponemica con este Id");
            }
            var pacienteDiagnosticoPruebaTreponemica = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");
            if (pacienteDiagnosticoPruebaTreponemica.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteDiagnosticoPruebaTreponemica con este Id");
            }
            return Created("ObtenerPacienteDiagnosticoPruebaTreponemica", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se obtuvo Id Solicitado", pacienteDiagnosticoPruebaTreponemica = pacienteDiagnosticoPruebaTreponemica });
        }

        [HttpPost]
        public async Task<ActionResult<PacienteDiagnosticoPruebaTreponemica>> PostPacienteDiagnosticoPruebaTreponemica(PacienteDiagnosticoPruebaTreponemica pacienteDiagnosticoPruebaTreponemica)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Modificado", "No fue posible Modificar PacienteDiagnosticoPruebaTreponemica");
            }

            await _service.CreateAsync(pacienteDiagnosticoPruebaTreponemica);

            return Created("CrearpacienteDiagnosticoPruebaTreponemica", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Creo el Paciente con Exito", pacienteDiagnosticoPruebaTreponemica = pacienteDiagnosticoPruebaTreponemica });
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteDiagnosticoPruebaTreponemica(long Id, PacienteDiagnosticoPruebaTreponemica pacienteDiagnosticoPruebaTreponemica)
        {
            if (Id != pacienteDiagnosticoPruebaTreponemica.Id)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No fue posible encontrar el Id del PacienteDiagnosticoPruebaTreponemica");
            }

            bool updated = await _service.UpdateAsync(Id, pacienteDiagnosticoPruebaTreponemica);

            if (!updated)
            {
                throw new ExcepcionError(HttpStatusCode.NotModified, "No Modificado", "No se pudo Actualizar el PacienteDiagnosticoPruebaTreponemica");
            }
            return Created("ActualizarPacienteDiagnosticoPruebaTreponemica", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Actualizo con Exito el PacienteDiagnosticoPruebaTreponemica" });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacienteDiagnosticoPruebaTreponemica(long Id)
        {
            var pacienteDiagnosticoPruebaTreponemica = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (pacienteDiagnosticoPruebaTreponemica.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteDiagnosticoPruebaTreponemica con este Id");
            }
            await _service.DeleteAsync(Id);
            return Created("EliminarPacienteDiagnosticoPruebaTreponemica", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Elimino con Exito el PacienteDiagnosticoPruebaTreponemica" });
        }
    }
}
