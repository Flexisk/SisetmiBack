namespace Dominio.Paciente
{
    public class PacienteAfiliacion
    {
        public long Id { get; set; }
        public long PacienteId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long UsuarioId { get; set; } 
        public long RegimenId { get; set; }
        public long AseguradoraId { get; set; }
        public String? VcOtraAseguradora { get; set; }
        public Paciente? Paciente { get; set; }
    }
}
