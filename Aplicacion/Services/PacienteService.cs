using Dominio.Request;
using Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Services
{
    public class PacienteService
    {
        private  PacienteRepository _pacienteRepository { get; }

        public PacienteService(PacienteRepository pacienteRepository)
        {
            this._pacienteRepository = pacienteRepository;
        }

        public async Task<PacienteRequest> crearPaciente(PacienteRequest pacienteRequest)
        {
            return await _pacienteRepository.crearPaciente(pacienteRequest);
        }
    }
}
