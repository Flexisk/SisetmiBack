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
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron afiliaciones de pacientes", Codigo = HttpStatusCode.OK };
            IEnumerable<PacienteAfiliacion> PacientesAfiliacionModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen afiliaciones de pacientes", Codigo = HttpStatusCode.Accepted };
            }
            else
            {
                PacientesAfiliacionModel = await _service.GetAsync();
            }


            var listModelResponse = new ListModelResponse<PacienteAfiliacion>(response.Codigo, response.Titulo, response.Mensaje, PacientesAfiliacionModel);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteAfiliacion>> GetPacienteAfiliacion(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            PacienteAfiliacion PacientesAfiliacionModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {

                response = new { Titulo = "Algo salio mal", Mensaje = "No existen afiliaciones de pacientes", Codigo = HttpStatusCode.BadRequest };

            }

            var Pacienteafiliacion = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (Pacienteafiliacion.Count < 1)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe  afiliaciones de pacientes con id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                PacientesAfiliacionModel = Pacienteafiliacion.First();
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo las  afiliaciones de pacientes con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<PacienteAfiliacion>(response.Codigo, response.Titulo, response.Mensaje, PacientesAfiliacionModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }

        [HttpPost]
        public async Task<ActionResult<PacienteAfiliacion>> PostPacienteAfiliacion(PacienteAfiliacion pacienteAfiliacion)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Afiliacion de paciente creado de forma correcta", Codigo = HttpStatusCode.Created };
            PacienteAfiliacion PacienteAfiliacionModel = null;


            bool guardo = await _service.CreateAsync(pacienteAfiliacion);
            if (!guardo)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se puedo guardar la afiliacion de paciente", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                PacienteAfiliacionModel = pacienteAfiliacion;
            }


            var modelResponse = new ModelResponse<PacienteAfiliacion>(response.Codigo, response.Titulo, response.Mensaje, PacienteAfiliacionModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteAfiliacion(long Id, PacienteAfiliacion pacienteAfiliacion)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se actualizó el afiliacion paciente de forma correcta", Codigo = HttpStatusCode.OK };

            if (Id != pacienteAfiliacion.Id)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El id de la afiliacion de paciente no corresponde con el del modelo", Codigo = HttpStatusCode.BadRequest };
            }
            else if (pacienteAfiliacion.Id < 1)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El modelo de afiliacion de paciente no tiene el campo Id ", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                var Pacienteafiliacion = await _service.FindAsync(Id);

                if (Pacienteafiliacion == null)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No existe afiliacion de paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
                }
                else
                {
                    bool updated = await _service.UpdateAsync(Id, pacienteAfiliacion);

                    if (!updated)
                    {
                        response = new { Titulo = "Algo salió mal!", Mensaje = "No fue posible actualizar la afiliacion de paciente", Codigo = HttpStatusCode.NoContent };
                    }
                }

            }

            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacienteAfiliacion(long Id)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se eliminó la afiliacion de paciente de forma correcta", Codigo = HttpStatusCode.OK };
            var Pacienteafiliacion = await _service.FindAsync(Id);

            if (Pacienteafiliacion == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe afiliacion de paciente con este id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                bool elimino = await _service.DeleteAsync(Id);
                if (!elimino)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "No se pudo eliminar la afiliacion de paciente con Id " + Id, Codigo = HttpStatusCode.NoContent };
                }
            }
            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);

        }

    }
}
