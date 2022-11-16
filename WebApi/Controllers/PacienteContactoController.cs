using Aplicacion.ManejadorErrores;
using Aplicacion.Services;
using Dominio.Paciente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Responses;

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
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron contacto de paciente", Codigo = HttpStatusCode.OK };
            IEnumerable<PacienteContacto> PacientescontactoModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe contacto de paciente", Codigo = HttpStatusCode.Accepted };
            }
            else
            {
                PacientescontactoModel = await _service.GetAsync();
            }


            var listModelResponse = new ListModelResponse<PacienteContacto>(response.Codigo, response.Titulo, response.Mensaje, PacientescontactoModel);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteContacto>> GetPacienteContacto(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            PacienteContacto PacientescontactoModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {

                response = new { Titulo = "Algo salio mal", Mensaje = "No existen contacto de paciente", Codigo = HttpStatusCode.BadRequest };

            }

            var Pacientecontacto = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (Pacientecontacto.Count < 1)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe contacto de paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                PacientescontactoModel = Pacientecontacto.First();
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo el contacto del paciente con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<PacienteContacto>(response.Codigo, response.Titulo, response.Mensaje, PacientescontactoModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }

        [HttpPost]
        public async Task<ActionResult<PacienteContacto>> PostPacienteContacto(PacienteContacto pacienteContacto)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Contacto de paciente creado de forma correcta", Codigo = HttpStatusCode.Created };
            PacienteContacto PacientecontactoModel = null;


            bool guardo = await _service.CreateAsync(pacienteContacto);
            if (!guardo)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se puedo guardar el contacto de paciente", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                PacientecontactoModel = pacienteContacto;
            }


            var modelResponse = new ModelResponse<PacienteContacto>(response.Codigo, response.Titulo, response.Mensaje, PacientecontactoModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteContacto(long Id, PacienteContacto pacienteContacto)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se actualizó el contacto del paciente de forma correcta", Codigo = HttpStatusCode.OK };

            if (Id != pacienteContacto.Id)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El id de contacto de paciente no corresponde con el del modelo", Codigo = HttpStatusCode.BadRequest };
            }
            else if (pacienteContacto.Id < 1)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El modelo de contacto paciente no tiene el campo Id ", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                var Pacientecontacto = await _service.FindAsync(Id);

                if (Pacientecontacto == null)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No existe contacto de paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
                }
                else
                {
                    bool updated = await _service.UpdateAsync(Id, pacienteContacto);

                    if (!updated)
                    {
                        response = new { Titulo = "Algo salió mal!", Mensaje = "No fue posible actualizar el contacto del paciente", Codigo = HttpStatusCode.NoContent };
                    }
                }

            }

            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacienteContacto(long Id)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se eliminó el contacto del paciente de forma correcta", Codigo = HttpStatusCode.OK };
            var Pacientecontacto = await _service.FindAsync(Id);

            if (Pacientecontacto == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe contacto del paciente con este id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                bool elimino = await _service.DeleteAsync(Id);
                if (!elimino)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "No se pudo eliminar el contacto del paciente con Id " + Id, Codigo = HttpStatusCode.NoContent };
                }
            }
            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);
        }
    }
}
