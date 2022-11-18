
namespace Dominio.Paciente
{
    public class PacienteContacto
    {
        public long Id { get; set; }
        public long PacienteId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long UsuarioId { get; set; }
        public long? PaisId { get; set; }
        public long DepartamentoId { get; set; }
        public long LocalidadId { get; set; }
        public long UpzId { get; set; }
        public long BarrioId { get; set; }
        public string? VcDireccionPrincipal { get; set; }
        public string? VcDireccionSecundaria { get; set; }
        public string? VcTelefono1 { get; set; }
        public string? VcTelefono2 { get; set; }

        public Paciente? Paciente { get; set; }

    }
}
