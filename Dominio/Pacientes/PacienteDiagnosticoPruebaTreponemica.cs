using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Pacientes
{
    public class PacienteDiagnosticoPruebaTreponemica
    {
        public long Id { get; set; }
        public long PacienteDiagnosticoId { get; set; }
        public string? TipoPruebaTriponemica { get; set; }
        public bool BResultadoPruebaTreponemica { get; set; }
        public DateTime DtResultadoPruebaTreponemica { get; set; }

        public PacienteDiagnostico? PacienteDiagnostico { get; set; }

    }
}
