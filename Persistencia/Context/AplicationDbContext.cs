
using Dominio.Paciente;
using Microsoft.EntityFrameworkCore;
using Persistencia.FluentConfig.PacientesConfig;

namespace Persistencia.Context
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options): base(options) { }

        public AplicationDbContext() { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new PacienteConfig(modelBuilder.Entity<PacienteRequest>());
            new PacienteCasoConfig(modelBuilder.Entity<PacienteCaso>());
            new PacienteDiagnosticoConfig(modelBuilder.Entity<PacienteDiagnostico>());

        }

        public DbSet<PacienteRequest> Pacientes { get; set; }
        public DbSet<PacienteAfiliacionRequest> PacienteAfiliacion { get; set; }
        public DbSet<PacienteContactoRequest> PacienteContacto { get; set; }
        public DbSet<PacienteDiagnostico> PacienteDiagnostico { get; set; }
        public DbSet<PacienteDiagnosticoPruebaTreponemica> PacienteDiagnosticoPruebaTreponemica { get; set; }
        public DbSet<PacienteCaso> PacienteCaso { get; set; }




    }
}
