using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Request
{
    public class PacienteRequest
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public string? VcDocumento { get; set; }
        public string? VcPrimerNombre { get; set; }
        public string? VcSegundoNombre { get; set; }
        public string? VcPrimerApellido { get; set; }
        public string? VcSegundoApellido { get; set; }
        public long NacionalidadId { get; set; }
        public DateTime DtFechaNacimineto { get; set; }

        //Datos compartidos entre Afiliación y Contacto
        public DateTime DtFechaRegistro { get; set; }
        public long UsuarioId { get; set; }

        //Datos de afiliación
        public long RegimenId { get; set; }
        public long AseguradoraId { get; set; }
        public string? VcOtraAseguradora { get; set; }

        //Datos de Contacto
        public long PaisId { get; set; }
        public long DepartamentoId { get; set; }
        public long LocalidadId { get; set; }
        public long UpzId { get; set; }
        public long BarrioId { get; set; }
        public string? VcDireccionPrincipal { get; set; }
        public string? VcDireccionSecundaria { get; set; }
        public string? VcTelefono1 { get; set; }
        public string? VcTelefono2 { get; set; }
    }
}
