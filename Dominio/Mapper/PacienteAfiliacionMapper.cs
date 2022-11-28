using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mapper
{
    public class PacienteAfiliacionMapper
    {
        public long Id { get; set; }
        public long PacienteId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long UsuarioId { get; set; }
        public long RegimenId { get; set; }
        public long AseguradoraId { get; set; }
        public String? VcOtraAseguradora { get; set; }
    }
}
