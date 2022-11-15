using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dominio.Paciente;
using System.Net;
using Aplicacion.Services;
using Aplicacion.ManejadorErrores;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IGenericService<Paciente> _service;
        public PacientesController(IGenericService<Paciente> service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPaciente()
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los Pacientes", Codigo = HttpStatusCode.OK };
            IEnumerable<Paciente> PacientesModel = null;
            if (!_service.ExistsAsync())
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen Pacientes", Codigo = HttpStatusCode.NoContent };
            }

            PacientesModel = await _service.GetAsync();

            var listModelResponse = new ListModelResponse<Paciente>(response.Codigo, response.Titulo, response.Mensaje, PacientesModel);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            Paciente PacientesModel = null;
            if (!_service.ExistsAsync())
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen actividades", Codigo = HttpStatusCode.BadRequest };
            }

            var pacientes = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (pacientes.Count < 1)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe Pacientes con id " + Id, Codigo = HttpStatusCode.NotFound };

            }
            else
            {
                PacientesModel = pacientes.First();
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo el Paciente con el Id solicitado", Codigo = HttpStatusCode.OK };
            }
            var modelResponse = new ModelResponse<Paciente>(response.Codigo, response.Titulo, response.Mensaje, PacientesModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente pacientes)
        {
            try
            {
                var response = new { Titulo = "Bien Hecho!", Mensaje = "Paceinte creado de forma correcta", Codigo = HttpStatusCode.Created };
                Paciente PacientesModel = null;

                //Debería obtener el modelo con el ID, en lugar de un bool
                bool guardo = await _service.CreateAsync(pacientes);
                if (!guardo)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No se puedo guardar el Paciente", Codigo = HttpStatusCode.BadRequest };
                }
                else
                {
                    PacientesModel = pacientes;
                }

                var modelResponse = new ModelResponse<Paciente>(response.Codigo, response.Titulo, response.Mensaje, PacientesModel);
                return StatusCode((int)modelResponse.Codigo, modelResponse);
            }
            catch (Exception ex)
            {
                throw;
            }


            //if (!_service.ExistsAsync())
            //{
            //    throw new ExcepcionError(HttpStatusCode.NotFound, "No Modificado", "No fue posible Modificar Paciente");
            //}
            //await _service.CreateAsync(pacientes);
            //return Created("CrearPaciente", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Creo el Paciente con Exito", pacientes = pacientes });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPacientes(long Id, Paciente pacientes)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se actualizó el Paciente de forma correcta", Codigo = HttpStatusCode.OK };
            try
            {
                if (Id != pacientes.Id)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "El id del Paciente no corresponde con el del modelo", Codigo = HttpStatusCode.BadRequest };
                }
                else if (pacientes.Id < 1)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "El modelo del Paciente no tiene el campo Id ", Codigo = HttpStatusCode.BadRequest };
                }
                else
                {
                    var Pacientes = await _service.FindAsync(Id);

                    if (Pacientes == null)
                    {
                        response = new { Titulo = "Algo salio mal", Mensaje = "No existe Paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
                    }
                    else
                    {
                        
                        bool updated = await _service.UpdateAsync(Id, pacientes);

                        if (!updated)
                        {
                            response = new { Titulo = "Algo salió mal!", Mensaje = "No fue posible actualizar la Paciente", Codigo = HttpStatusCode.NoContent };
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                throw;
            }
            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);


            //if (Id != pacientes.Id)
            //{
            //    throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No fue posible encontrar el Id del Paciente");
            //}

            //bool updated = await _service.UpdateAsync(Id, pacientes);

            //if (!updated)
            //{
            //    throw new ExcepcionError(HttpStatusCode.NotModified, "No Modificado", "No se pudo Actualizar el Paciente");
            //}
            //return Created("ActualizarPaciente", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Actualizo con Exito el Paciente" });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePacientes(long Id)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se eliminó el Paciente de forma correcta", Codigo = HttpStatusCode.OK };
            var Paciente = await _service.FindAsync(Id);

            if (Paciente == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe Paciente con id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                bool elimino = await _service.DeleteAsync(Id);
                if (!elimino)
                {
                    response = new { Titulo = "Algo salió mal!", Mensaje = "No se pudo eliminar el Paciente con Id " + Id, Codigo = HttpStatusCode.NoContent };
                }
            }
            var updateResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);
            return StatusCode((int)updateResponse.Codigo, updateResponse);



            //var pacientes = await _service.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");
            //if (pacientes.Count < 1)
            //{
            //    throw new ExcepcionError(HttpStatusCode.NotFound, "No Existe", "No se encontraron Pacientes con este Id");
            //}
            //await _service.DeleteAsync(Id);
            //return Created("EliminarPacientes", new { Codigo = HttpStatusCode.OK, Titulo = "OK", Mensaje = "Se Elimino con Exito el Paciente" });
        }
    }
}
