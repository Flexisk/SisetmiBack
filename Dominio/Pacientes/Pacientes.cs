namespace Dominio.Pacientes
{
    public class Pacientes
    {
        public long Id { get; set; }
        public string? TipoDocumentoId { get; set; }
        public string? VcDocumento { get; set; }
        public string? VcPrimerNombre { get; set; }
        public string? VcSegundoNombre { get; set; }
        public string? VcPrimerApellido { get; set; }
        public string? VcSegundoApellido { get; set; }
        public string? Nacionalidad { get; set; }
        public DateTime DtFechaNacimineto { get; set; }

        public virtual ICollection <PacienteAfiliacion> PacienteAfiliacion { get; set; }

        public virtual ICollection<PacienteContacto> PacienteContacto { get; set; }   

        public virtual ICollection<PacienteCaso> PacienteCaso { get; set; }

    }
}
