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
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron casos de pacientes", Codigo = HttpStatusCode.OK };
            IEnumerable<PacienteCaso> PacientescasoModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen casos de pacientes", Codigo = HttpStatusCode.Accepted };
            }
            else
            {
                PacientescasoModel = await _service.GetAsync();
            }


            var listModelResponse = new ListModelResponse<PacienteCaso>(response.Codigo, response.Titulo, response.Mensaje, PacientescasoModel);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteCaso>> GetPacienteCaso(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            PacienteCaso PacientescasoModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {

                response = new { Titulo = "Algo salio mal", Mensaje = "No existen casos de pacientes", Codigo = HttpStatusCode.BadRequest };

            }

            var Pacientecasos = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (Pacientecasos.Count < 1)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe casos de pacientes con id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                PacientescasoModel = Pacientecasos.First();
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvieron los casos de pacientes con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<PacienteCaso>(response.Codigo, response.Titulo, response.Mensaje, PacientescasoModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }

        [HttpPost]
        public async Task<ActionResult<PacienteCaso>> PostPacienteCaso(PacienteCaso pacienteCaso)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Caso del paciente creado de forma correcta", Codigo = HttpStatusCode.Created };
            PacienteCaso PacientecasoModel = null;


            bool guardo = await _service.CreateAsync(pacienteCaso);
            if (!guardo)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar el caso del paceinte", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                PacientecasoModel = pacienteCaso;
            }


            var modelResponse = new ModelResponse<PacienteCaso>(response.Codigo, response.Titulo, response.Mensaje, PacientecasoModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteCaso(long Id, PacienteCaso pacienteCaso)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se actualizó el caso del paciente de forma correcta", Codigo = HttpStatusCode.OK };

            if (Id != pacienteCaso.Id)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El id del caso del paciente no corresponde con el del modelo", Codigo = HttpStatusCode.BadRequest };
            }
            else if (pacienteCaso.Id < 1)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El modelo de caso del paciente no tiene el campo Id ", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                var Pacientecaso = await _service.FindAsync(Id);

                if (Pacientecaso == null)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No existe caso del paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
                }
                else
                {
                    bool updated = await _service.UpdateAsync(Id, pacienteCaso);

                    if (!updated)
                    {
                        response = new { Titulo = "Algo salió mal!", Mensaje = "No fue posible actualizar el caso del paciente", Codigo = HttpStatusCode.NoContent };
                    }
                }

            }

            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacienteCaso(long Id)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se eliminó el caso del paciente de forma correcta", Codigo = HttpStatusCode.OK };
            var Pacientecaso = await _service.FindAsync(Id);

            if (Pacientecaso == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe el caso del paciente con este id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                bool elimino = await _service.DeleteAsync(Id);
                if (!elimino)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "No se pudo eliminar el caso del paciente con id " + Id, Codigo = HttpStatusCode.NoContent };
                }
            }
            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);

        }
    }
}
