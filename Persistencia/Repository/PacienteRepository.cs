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
using Dominio.Mapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                NacionalidadId = pacienteRequest.NacionalidadId,
                DtFechaNacimineto = pacienteRequest.DtFechaNacimineto,
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

        //public IEnumerable<PacienteContactoMapper> getPacienteConsultar(long tipoDocumentoId, string vcDocumento)
        //{
        //    var parametrocontacto = _context.PacienteContacto.OrderByDescending(t => t.PacienteId).FirstOrDefault();

        //    var parametroafilacion = _context.PacienteAfiliacion.OrderByDescending(t => t.PacienteId).FirstOrDefault();

        //    var parametro = _context.Pacientes
        //           .Where(p => p.TipoDocumentoId == tipoDocumentoId && p.VcDocumento == vcDocumento)
        //           .Select(e=> new PacienteMapper
        //           {
        //                Id = e.Id,
        //                TipoDocumentoId = e.TipoDocumentoId,
        //                VcDocumento = e.VcDocumento,
        //                VcPrimerNombre = e.VcPrimerNombre,
        //                VcSegundoNombre = e.VcSegundoNombre,
        //                VcPrimerApellido = e.VcPrimerApellido,
        //                VcSegundoApellido = e.VcSegundoApellido,
        //                NacionalidadId = e.NacionalidadId,
        //                DtFechaNacimineto = e.DtFechaNacimineto,

        //                Parametroscontacto = e.Parametroscontacto.
        //                    Select(i=> new PacienteContactoMapper
        //                    {


        //                    })
                        



        //                  //.Select(d => new PacienteContactoMapper
        //                  //{
        //                  //    Id = parametrocontacto.Id,
        //                  //    PacienteId = parametrocontacto.PacienteId,


        //           //}
        //           //)

        //           }
        //           .OrderByDescending(t => t.Id).FirstOrDefault();
        //            return parametro.Parametroscontacto;


        //    //var pacienteRequest = new PacienteRequest

        //    //{
        //    //    //Datos de Paciente
        //    //    Id = parametro.Id,
        //    //    TipoDocumentoId = parametro.TipoDocumentoId,
        //    //    VcDocumento = parametro.VcDocumento,
        //    //    VcPrimerNombre = parametro.VcPrimerNombre,
        //    //    VcSegundoNombre = parametro.VcSegundoNombre,
        //    //    VcPrimerApellido = parametro.VcPrimerApellido,
        //    //    VcSegundoApellido = parametro.VcSegundoApellido,
        //    //    NacionalidadId = parametro.NacionalidadId,
        //    //    DtFechaNacimineto = parametro.DtFechaNacimineto,

        //    //    //Datos de Paciente contacto
        //    //    PacienteId = parametrocontacto.PacienteId,
        //    //    PaisId = (long)parametrocontacto.PaisId,
        //    //    DepartamentoId = parametrocontacto.DepartamentoId,
        //    //    LocalidadId = parametrocontacto.LocalidadId,
        //    //    UpzId = parametrocontacto.UpzId,
        //    //    BarrioId = parametrocontacto.BarrioId,
        //    //    VcDireccionPrincipal = parametrocontacto.VcDireccionPrincipal,
        //    //    VcDireccionSecundaria = parametrocontacto.VcDireccionSecundaria,
        //    //    VcTelefono1 = parametrocontacto.VcTelefono1,
        //    //    VcTelefono2 = parametrocontacto.VcTelefono2,

        //    //    //Datos de Paciente Afiliacon
        //    //    PacienteIdAfiliacion = parametroafilacion.PacienteId,
        //    //    RegimenId = parametroafilacion.RegimenId,
        //    //    AseguradoraId = parametroafilacion.AseguradoraId,
        //    //    VcOtraAseguradora = parametroafilacion.VcOtraAseguradora,

        //    //};

        //    //return Parametroscontacto;

        //}

        //public PacienteRequest getPacienteConsultar(long tipoDocumentoId, string vcDocumento)
        //{

        //    var parametro = _context.Pacientes
        //           .Where(p => p.TipoDocumentoId == tipoDocumentoId && p.VcDocumento == vcDocumento)
        //           .OrderByDescending(t => t.Id).FirstOrDefault();

        //    var parametrocontacto = _context.PacienteContacto.OrderByDescending(t => t.PacienteId).FirstOrDefault();

        //    var parametroafilacion = _context.PacienteAfiliacion.OrderByDescending(t => t.PacienteId).FirstOrDefault();

        //    var pacienteRequest = new PacienteRequest         

        //    {
        //        //Datos de Paciente
        //        Id = parametro.Id,
        //        TipoDocumentoId = parametro.TipoDocumentoId,
        //        VcDocumento = parametro.VcDocumento,
        //        VcPrimerNombre = parametro.VcPrimerNombre,
        //        VcSegundoNombre = parametro.VcSegundoNombre,
        //        VcPrimerApellido = parametro.VcPrimerApellido,
        //        VcSegundoApellido = parametro.VcSegundoApellido,
        //        NacionalidadId = parametro.NacionalidadId,
        //        DtFechaNacimineto = parametro.DtFechaNacimineto,

        //        //Datos de Paciente contacto
        //        PacienteId = parametrocontacto.PacienteId,
        //        PaisId = (long)parametrocontacto.PaisId,
        //        DepartamentoId = parametrocontacto.DepartamentoId,
        //        LocalidadId = parametrocontacto.LocalidadId,
        //        UpzId = parametrocontacto.UpzId,
        //        BarrioId = parametrocontacto.BarrioId,
        //        VcDireccionPrincipal = parametrocontacto.VcDireccionPrincipal,
        //        VcDireccionSecundaria = parametrocontacto.VcDireccionSecundaria,
        //        VcTelefono1 = parametrocontacto.VcTelefono1,
        //        VcTelefono2 = parametrocontacto.VcTelefono2,

        //        //Datos de Paciente Afiliacon
        //        PacienteIdAfiliacion = parametroafilacion.PacienteId,
        //        RegimenId = parametroafilacion.RegimenId,
        //        AseguradoraId = parametroafilacion.AseguradoraId,
        //        VcOtraAseguradora = parametroafilacion.VcOtraAseguradora,

        //    };

        //    return pacienteRequest;

        //}

        //public PacienteRequest getPacienteConsultarContacto(long PacienteId)
        //{
        //    var parametro = _context.PacienteContacto
        //            .Where(p => p.PacienteId == PacienteId)
        //            .OrderByDescending(t => t.Id).FirstOrDefault();

        //    if (parametro == null)
        //        return null;

        //    var pacienteRequest = new PacienteRequest

        //    {
        //        PacienteId = parametro.PacienteId,
        //        PaisId = (long)parametro.PaisId,
        //        DepartamentoId = (long)parametro.DepartamentoId,
        //        LocalidadId = (long)parametro.LocalidadId,
        //        UpzId = (long)parametro.UpzId,
        //        BarrioId = (long)parametro.BarrioId,
        //        VcDireccionPrincipal = parametro.VcDireccionPrincipal,
        //        VcDireccionSecundaria = parametro.VcDireccionSecundaria,
        //        VcTelefono1 = parametro.VcTelefono1,
        //        VcTelefono2 = parametro.VcTelefono2,

        //    };

        //    return pacienteRequest;

        //}

    }
    
}
