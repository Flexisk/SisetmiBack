using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using Dominio.Paciente;
using Microsoft.Data.SqlClient;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Request;

namespace Persistencia.Repository
{
    public class PacienteRepository :GenericRepository<Paciente>
    {
        public IGenericRepository<Paciente> _PacienteRepository { get; }
        public AplicationDbContext _context;

        public PacienteRepository(AplicationDbContext context, IGenericRepository<Paciente> parametroDetalleRepository) : base(context)
        {
            this._PacienteRepository = parametroDetalleRepository;
            this._context = context;
        }

        public async Task<PacienteRequest> crearPaciente(PacienteRequest pacienteRequest)
        {
            var registros = 0;
            var paciente = new Paciente
            {
                TipoDocumentoId = pacienteRequest.TipoDocumentoId,
                VcDocumento = pacienteRequest.VcDocumento,
                VcPrimerNombre = pacienteRequest.VcPrimerNombre,
                VcSegundoNombre = pacienteRequest.VcSegundoNombre,
                VcPrimerApellido = pacienteRequest.VcPrimerApellido,
                VcSegundoApellido = pacienteRequest.VcSegundoApellido,
                NacionalidadId= pacienteRequest.NacionalidadId,
                DtFechaNacimineto =pacienteRequest.DtFechaNacimineto,
            };
            _context.Pacientes.Add(paciente);
            registros = await _context.SaveChangesAsync();

            pacienteRequest.Id = paciente.Id;
            var pacienteAfiliacion = new PacienteAfiliacion
            {
                PacienteId = paciente.Id,
                RegimenId =pacienteRequest.RegimenId,
                AseguradoraId = pacienteRequest.AseguradoraId,
                VcOtraAseguradora = pacienteRequest.VcOtraAseguradora,
                DtFechaRegistro = DateTime.Now,
                UsuarioId =1,
            };

            _context.PacienteAfiliacion.Add(pacienteAfiliacion);

            var pacienteContacto = new PacienteContacto
            {
                PacienteId = paciente.Id,
                PaisId = pacienteRequest.PaisId,
                DepartamentoId = pacienteRequest.DepartamentoId,
                LocalidadId = pacienteRequest.LocalidadId,
                UpzId = pacienteRequest.UpzId,
                BarrioId = pacienteRequest.BarrioId,
                VcDireccionPrincipal = pacienteRequest.VcDireccionPrincipal,
                VcDireccionSecundaria = pacienteRequest.VcDireccionSecundaria,
                VcTelefono1 = pacienteRequest.VcTelefono1,
                VcTelefono2 = pacienteRequest.VcTelefono2,
                DtFechaRegistro = DateTime.Now,
                UsuarioId = 1,
            };

            _context.PacienteContacto.Add(pacienteContacto);



            registros+=await _context.SaveChangesAsync();


            return pacienteRequest;
        }
    }
    
}
