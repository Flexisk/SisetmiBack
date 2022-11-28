using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mapper
{
    public class PacienteContactoMapper
    {        
        public long Id { get; set; }
        public long PacienteId { get; set; }
        public String VcNumeroCaso { get; set; }
        public bool BComfirmadoSifilisGestacional { get; set; }
        public String? ClasificacionSifilis { get; set; }
        public DateTime DtFecha { get; set; }
    }
}
