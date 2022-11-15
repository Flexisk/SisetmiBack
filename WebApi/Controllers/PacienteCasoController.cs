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
    public class PacienteCasoController : Controller
    {
        private readonly IGenericService<PacienteCaso> _service;
        public PacienteCasoController(IGenericService<PacienteCaso> service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteCaso>>> GetPacienteCaso()
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NoContent, "No Existe", "No se encontraron PacienteCaso en Base de Datos");
            }
            return await _service.GetAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteCaso>> GetPacienteCaso(long Id)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteCaso con este Id");
            }
            var pacienteCaso = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");
            if (pacienteCaso.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteCaso con este Id");
            }
            return Created("ObtenerPacienteCaso", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se obtuvo Id Solicitado", pacienteCaso = pacienteCaso });
        }

        [HttpPost]
        public async Task<ActionResult<PacienteCaso>> PostPacienteCaso(PacienteCaso pacienteCaso)
        {
            if (!_service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Modificado", "No fue posible Modificar PacienteCaso");
            }

            await _service.CreateAsync(pacienteCaso);

            return Created("CrearPaciente", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Creo el PacienteCaso con Exito", pacienteCaso = pacienteCaso });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteCaso(long Id, PacienteCaso pacienteCaso)
        {
            if (Id != pacienteCaso.Id)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No fue posible encontrar el Id del PacienteCaso");
            }

            bool updated = await _service.UpdateAsync(Id, pacienteCaso);

            if (!updated)
            {
                throw new ExcepcionError(HttpStatusCode.NotModified, "No Modificado", "No se pudo Actualizar el PacienteCaso");
            }
            return Created("ActualizarPacienteCaso", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Actualizo con Exito el PacienteCaso" });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacienteCaso(long Id)
        {
            var pacienteCaso = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (pacienteCaso.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteCaso con este Id");
            }
            await _service.DeleteAsync(Id);
            return Created("EliminarpacienteCaso", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Elimino con Exito el PacienteCaso" });
        }
    }
}
