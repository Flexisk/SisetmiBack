using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dominio.Paciente;
using System.Net;
using Aplicacion.Services;
using Aplicacion.ManejadorErrores;
using WebApi.Responses;
using Dominio.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IGenericService<Paciente> _service;
        private readonly PacienteService _pacienteService;
        public PacientesController(IGenericService<Paciente> service, PacienteService pacienteService)
        {
            this._service = service;
            this._pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPaciente()
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los Pacientes", Codigo = HttpStatusCode.OK };
            IEnumerable<Paciente> PacientesModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen los Pacientes", Codigo = HttpStatusCode.Accepted };
            }
            else
            {
                PacientesModel = await _service.GetAsync();
            }


            var listModelResponse = new ListModelResponse<Paciente>(response.Codigo, response.Titulo, response.Mensaje, PacientesModel);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            Paciente PacientesModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {

                response = new { Titulo = "Algo salio mal", Mensaje = "No existen pacientes", Codigo = HttpStatusCode.BadRequest };

            }

            var Paciente = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (Paciente.Count < 1)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe pacientes con id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                PacientesModel = Paciente.First();
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo los pacientes con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<Paciente>(response.Codigo, response.Titulo, response.Mensaje, PacientesModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }

        [HttpPost]
        public async Task<ActionResult<PacienteRequest>> PostPaciente(PacienteRequest pacienteRequest)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Paciente creado de forma correcta", Codigo = HttpStatusCode.Created };
            PacienteRequest PacienteModel = null;


            PacienteModel = await _pacienteService.crearPaciente(pacienteRequest);
            if (PacienteModel.Id > 0)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se puedo guardar el paciente", Codigo = HttpStatusCode.BadRequest };
            }
                


            var modelResponse = new ModelResponse<PacienteRequest>(response.Codigo, response.Titulo, response.Mensaje, PacienteModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacientes(long Id, Paciente pacientes)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se actualizó el paciente de forma correcta", Codigo = HttpStatusCode.OK };
      
                if (Id != pacientes.Id)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "El id del paciente no corresponde con el del modelo", Codigo = HttpStatusCode.BadRequest };
                }
                else if (pacientes.Id < 1)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "El modelo de paciente no tiene el campo Id ", Codigo = HttpStatusCode.BadRequest };
                }
                else
                {
                    var Paciente = await _service.FindAsync(Id);

                    if (Paciente == null)
                    {
                        response = new { Titulo = "Algo salio mal", Mensaje = "No existe paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
                    }
                    else
                    {
                        bool updated = await _service.UpdateAsync(Id, pacientes);

                        if (!updated)
                        {
                            response = new { Titulo = "Algo salió mal!", Mensaje = "No fue posible actualizar el paciente", Codigo = HttpStatusCode.NoContent };
                        }
                    }

                }
       
            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacientes(long Id)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se eliminó el paciente de forma correcta", Codigo = HttpStatusCode.OK };
            var Paciente = await _service.FindAsync(Id);

            if (Paciente == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe paciente con este id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                bool elimino = await _service.DeleteAsync(Id);
                if (!elimino)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "No se pudo eliminar el paciente con Id " + Id, Codigo = HttpStatusCode.NoContent };
                }
            }
            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);

        }
    }
}
