namespace Dominio.Paciente
{
    public class Paciente
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public String? VcDocumento { get; set; }
        public String? VcPrimerNombre { get; set; }
        public String? VcSegundoNombre { get; set; }
        public String? VcPrimerApellido { get; set; }
        public String? VcSegundoApellido { get; set; }
        public long NacionalidadId { get; set; }
        public DateTime DtFechaNacimineto { get; set; }

        public virtual ICollection <PacienteAfiliacion>? PacienteAfiliacion { get; set; }

        public virtual ICollection<PacienteContacto>? PacienteContacto { get; set; }   

        public virtual ICollection<PacienteCaso>? PacienteCaso { get; set; }

        public object last()
        {
            throw new NotImplementedException();
        }
    }
}
