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
    public class PacienteContactoController : Controller
    {
        private readonly IGenericService<PacienteContacto> _service;
        public PacienteContactoController(IGenericService<PacienteContacto> service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteContacto>>> GetPacienteContacto()
        {
            if (!await _service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NoContent, "No Existe", "No se encontraron PacienteContacto en Base de Datos");
            }
            return await _service.GetAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteContacto>> GetPacienteContacto(long Id)
        {
            if (!await _service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteContacto con este Id");
            }
            var pacienteContacto = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");
            if (pacienteContacto.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteContacto con este Id");
            }
            return Created("ObtenerPacienteContacto", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se obtuvo Id Solicitado", pacienteContacto = pacienteContacto });
        }

        [HttpPost]
        public async Task<ActionResult<PacienteContacto>> PostPacienteContacto(PacienteContacto pacienteContacto)
        {
            if (!await _service.ExistsAsync())
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Modificado", "No fue posible Modificar PacienteContacto");
            }

            await _service.CreateAsync(pacienteContacto);

            return Created("CrearPacienteContacto", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Creo el PacienteContacto con Exito", pacienteContacto = pacienteContacto });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteContacto(long Id, PacienteContacto pacienteContacto)
        {
            if (Id != pacienteContacto.Id)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No fue posible encontrar el Id del PacienteContacto");
            }

            bool updated = await _service.UpdateAsync(Id, pacienteContacto);

            if (!updated)
            {
                throw new ExcepcionError(HttpStatusCode.NotModified, "No Modificado", "No se pudo Actualizar el PacienteContacto");
            }
            return Created("ActualizarPacienteContacto", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Actualizo con Exito el PacienteContacto" });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacienteContacto(long Id)
        {
            var pacienteContacto = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (pacienteContacto.Count < 1)
            {
                throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron PacienteContacto con este Id");
            }
            await _service.DeleteAsync(Id);
            return Created("EliminarPacienteContacto", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Elimino con Exito el PacienteContacto" });
        }
    }
}
