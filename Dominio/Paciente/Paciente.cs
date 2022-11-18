namespace Dominio.Paciente
{
    public class Paciente
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

        public virtual ICollection <PacienteAfiliacion>? PacienteAfiliacion { get; set; }

        public virtual ICollection<PacienteContacto>? PacienteContacto { get; set; }   

        public virtual ICollection<PacienteCaso>? PacienteCaso { get; set; }

    }
}
