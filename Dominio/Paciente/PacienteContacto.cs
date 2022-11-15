
namespace Dominio.Paciente
{
    public class PacienteContacto
    {
        public long Id { get; set; }
        public long PacienteId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long UsuarioId { get; set; }
        public string? PaisId { get; set; }
        public long DepartamentoId { get; set; }
        public long LocalidadId { get; set; }
        public long UpzId { get; set; }
        public long BarrioId { get; set; }
        public string? VcDireccionPrincipal { get; set; }
        public string? VcDireccionSecundaria { get; set; }
        public int VcTelefono1 { get; set; }
        public int VcTelefono2 { get; set; }

        public Paciente? Paciente { get; set; }

    }
}
