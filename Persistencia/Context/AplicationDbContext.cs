
using Dominio.Pacientes;
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

            new PacienteConfig(modelBuilder.Entity<Pacientes>());
            new PacienteCasoConfig(modelBuilder.Entity<PacienteCaso>());
            new PacienteDiagnosticoConfig(modelBuilder.Entity<PacienteDiagnostico>());

        }

        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<PacienteAfiliacion> PacienteAfiliacion { get; set; }
        public DbSet<PacienteContacto> PacienteContacto { get; set; }
        public DbSet<PacienteDiagnostico> PacienteDiagnostico { get; set; }
        public DbSet<PacienteDiagnosticoPruebaTreponemica> PacienteDiagnosticoPruebaTreponemica { get; set; }
        public DbSet<PacienteCaso> PacienteCaso { get; set; }




    }
}
