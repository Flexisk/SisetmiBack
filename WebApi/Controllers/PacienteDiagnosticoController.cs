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
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los diagnosticos del paciente", Codigo = HttpStatusCode.OK };
            IEnumerable<PacienteDiagnostico> PacientesdiagnosticoModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen los diagnosticos del paciente", Codigo = HttpStatusCode.Accepted };
            }
            else
            {
                PacientesdiagnosticoModel = await _service.GetAsync();
            }


            var listModelResponse = new ListModelResponse<PacienteDiagnostico>(response.Codigo, response.Titulo, response.Mensaje, PacientesdiagnosticoModel);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteDiagnostico>> GetPacienteDiagnostico(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            PacienteDiagnostico PacientesdiagnosticoModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {

                response = new { Titulo = "Algo salio mal", Mensaje = "No existen diagnosticos paciente", Codigo = HttpStatusCode.BadRequest };

            }

            var Pacientediagnostico = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (Pacientediagnostico.Count < 1)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe diagnosticos de paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                PacientesdiagnosticoModel = Pacientediagnostico.First();
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo los diagnosticos de paciente con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<PacienteDiagnostico>(response.Codigo, response.Titulo, response.Mensaje, PacientesdiagnosticoModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }

        [HttpPost]
        public async Task<ActionResult<PacienteDiagnostico>> PostPacienteDiagnostico(PacienteDiagnostico pacienteDiagnostico)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Paciente diagnostico creado de forma correcta", Codigo = HttpStatusCode.Created };
            PacienteDiagnostico PacientediagnosticoModel = null;


            bool guardo = await _service.CreateAsync(pacienteDiagnostico);
            if (!guardo)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar el paciente diagnostico", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                PacientediagnosticoModel = pacienteDiagnostico;
            }


            var modelResponse = new ModelResponse<PacienteDiagnostico>(response.Codigo, response.Titulo, response.Mensaje, PacientediagnosticoModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        } 

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteDiagnostico(long Id, PacienteDiagnostico pacienteDiagnostico)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se actualizó el diagnostico de paciente  de forma correcta", Codigo = HttpStatusCode.OK };

            if (Id != pacienteDiagnostico.Id)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El id del diagnostico de paciente  no corresponde con el del modelo", Codigo = HttpStatusCode.BadRequest };
            }
            else if (pacienteDiagnostico.Id < 1)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El modelo de diagnostico del paciente no tiene el campo Id ", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                var Pacientediagnostico = await _service.FindAsync(Id);

                if (Pacientediagnostico == null)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No existe diagnostico de paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
                }
                else
                {
                    bool updated = await _service.UpdateAsync(Id, pacienteDiagnostico);

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
        public async Task<IActionResult> DeletePacienteDiagnostico(long Id)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se eliminó el diagnostico de paciente de forma correcta", Codigo = HttpStatusCode.OK };
            var Pacientediagnostico = await _service.FindAsync(Id);

            if (Pacientediagnostico == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe el diagnostico de paciente con este id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                bool elimino = await _service.DeleteAsync(Id);
                if (!elimino)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "No se pudo eliminar el diagnostico de paciente con Id " + Id, Codigo = HttpStatusCode.NoContent };
                }
            }
            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);

        }
    }
}
