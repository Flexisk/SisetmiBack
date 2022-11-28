using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mapper
{
    public class PacienteMapper
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public string? VcDocumento { get; set; }
        public String? VcPrimerNombre { get; set; }
        public String? VcSegundoNombre { get; set; }
        public String? VcPrimerApellido { get; set; }
        public String? VcSegundoApellido { get; set; }
        public long NacionalidadId { get; set; }
        public DateTime DtFechaNacimineto { get; set; }

        public IEnumerable<PacienteContactoMapper> PacienteContactosMapper { get; set; }
        = Array.Empty<PacienteContactoMapper>();

        public PacienteAfiliacionMapper PacienteAfiliacionMapper { get; set; }


        // public IEnumerable<PacienteAfiliacionMapper> PacienteAfiliaciones { get; set; }
        //= Array.Empty<PacienteAfiliacionMapper>();


    }

}