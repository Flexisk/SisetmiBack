using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Pacientes
{
    public class PacienteCaso
    {
        public long Id { get; set; }
        public long PacienteId { get; set; }
        public int VcNumeroCaso { get; set; }
        public bool BComfirmadoSifilisGestacional { get; set; }
        public string? ClasificacionSifilis { get; set; }
        public DateTime DtFecha { get; set; }

        public Pacientes? Pacientes { get; set; }
        public virtual ICollection<PacienteDiagnostico>? PacienteDiagnostico { get; set; }

    }
}
