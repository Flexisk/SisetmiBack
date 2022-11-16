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
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron diagnostico prueba treponemica de paciente", Codigo = HttpStatusCode.OK };
            IEnumerable<PacienteDiagnosticoPruebaTreponemica> PacientespruebatreponemicaModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen diagnostico de prueba treponemica de paciente", Codigo = HttpStatusCode.Accepted };
            }
            else
            {
                PacientespruebatreponemicaModel = await _service.GetAsync();
            }


            var listModelResponse = new ListModelResponse<PacienteDiagnosticoPruebaTreponemica>(response.Codigo, response.Titulo, response.Mensaje, PacientespruebatreponemicaModel);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PacienteDiagnosticoPruebaTreponemica>> GetPacienteDiagnosticoPruebaTreponemica(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            PacienteDiagnosticoPruebaTreponemica PacientespruebatreponemicaModel = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {

                response = new { Titulo = "Algo salio mal", Mensaje = "No existen diagnosticos prueba treponemica", Codigo = HttpStatusCode.BadRequest };

            }

            var Pacientepruebatreponemica = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (Pacientepruebatreponemica.Count < 1)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe diagnosticos prueba treponemica con id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                PacientespruebatreponemicaModel = Pacientepruebatreponemica.First();
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo los diagnosticos prueba treponemica del paciente con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<PacienteDiagnosticoPruebaTreponemica>(response.Codigo, response.Titulo, response.Mensaje, PacientespruebatreponemicaModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }

        [HttpPost]
        public async Task<ActionResult<PacienteDiagnosticoPruebaTreponemica>> PostPacienteDiagnosticoPruebaTreponemica(PacienteDiagnosticoPruebaTreponemica pacienteDiagnosticoPruebaTreponemica)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Diagnostico prueba treponemica de paciente creado de forma correcta", Codigo = HttpStatusCode.Created };
            PacienteDiagnosticoPruebaTreponemica PacientepruebatreponemicaModel = null;


            bool guardo = await _service.CreateAsync(pacienteDiagnosticoPruebaTreponemica);
            if (!guardo)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se puedo guardar el diagnostico prueba treponemica del paciente", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                PacientepruebatreponemicaModel = pacienteDiagnosticoPruebaTreponemica;
            }


            var modelResponse = new ModelResponse<PacienteDiagnosticoPruebaTreponemica>(response.Codigo, response.Titulo, response.Mensaje, PacientepruebatreponemicaModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacienteDiagnosticoPruebaTreponemica(long Id, PacienteDiagnosticoPruebaTreponemica pacienteDiagnosticoPruebaTreponemica)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se actualizó la prueba treponemica del paciente de forma correcta", Codigo = HttpStatusCode.OK };

            if (Id != pacienteDiagnosticoPruebaTreponemica.Id)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El id de la prueba treponemica no corresponde con el del modelo", Codigo = HttpStatusCode.BadRequest };
            }
            else if (pacienteDiagnosticoPruebaTreponemica.Id < 1)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "El modelo de la prueba treponemica no tiene el campo Id ", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                var Pacientepruebatreponemica = await _service.FindAsync(Id);

                if (Pacientepruebatreponemica == null)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No existe prueba treponemica de paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
                }
                else
                {
                    bool updated = await _service.UpdateAsync(Id, pacienteDiagnosticoPruebaTreponemica);

                    if (!updated)
                    {
                        response = new { Titulo = "Algo salió mal!", Mensaje = "No fue posible actualizar el prueba treponemica del paciente", Codigo = HttpStatusCode.NoContent };
                    }
                }

            }

            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacienteDiagnosticoPruebaTreponemica(long Id)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se eliminó el prueba treponemica de forma correcta", Codigo = HttpStatusCode.OK };
            var Pacientepruebatreponemica = await _service.FindAsync(Id);

            if (Pacientepruebatreponemica == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe prueba treponemica con este id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                bool elimino = await _service.DeleteAsync(Id);
                if (!elimino)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "No se pudo eliminar el prueba treponemica con Id " + Id, Codigo = HttpStatusCode.NoContent };
                }
            }
            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);

        }
    }
}
