using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Paciente
{
    public class PacienteDiagnostico
    {
        public long Id { get; set; }
        public long PacienteCasoId { get; set; }
        public DateTime DtFechaDiagnostico { get; set; }
        public long IpsDiagnosticaId { get; set; }
        public long FormulaObstetricaId { get; set; }
        public long CondicionActualId { get; set; }
        public string? VcEdadGestacional { get; set; }
        public long AntecedenteItsId { get; set; }
        public long TipoAntecedenteId { get; set; }
        public string? VcDescripcionAntecedente { get; set; }
        public long AntecedentePenicilinaId { get; set; }
        public bool BRequierePruebaNoTreponemica { get; set; }
        public string? TipoPruebaNoTriponemica { get; set; }
        public string? ResultadoPruebaNoTriponemica { get; set; }
        public DateTime DtResultadoPruebaNoTreponemica { get; set; }
        public long ModificacionDefinicionCasoId { get; set; }

        public virtual ICollection<PacienteDiagnosticoPruebaTreponemica>? PacienteDiagnosticoPruebaTreponemica { get; set; }

        public PacienteCaso? PacienteCaso { get; set; }

    }
}
