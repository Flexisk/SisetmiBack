using Dominio.Paciente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.FluentConfig.PacientesConfig
{
    public class PacienteDiagnosticoPruebaTreponemicaConfig
    {
        public PacienteDiagnosticoPruebaTreponemicaConfig(EntityTypeBuilder<PacienteDiagnosticoPruebaTreponemica> entity)
        {
            entity.ToTable("PacienteDiagnosticoPruebaTreponemica")
                .HasKey(p => p.Id);

            entity
                .HasOne(p => p.PacienteDiagnostico)
                .WithMany(p => p.PacienteDiagnosticoPruebaTreponemica)
                .HasForeignKey(p => p.PacienteDiagnosticoId)
                .HasConstraintName("FK_Paciente_Diagnostico_Prueba_Triponemica")
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.TipoPruebaTriponemica).IsRequired().HasMaxLength(100);
            entity.Property(p => p.BResultadoPruebaTreponemica).IsRequired().HasMaxLength(20);
            entity.Property(p => p.DtResultadoPruebaTreponemica).IsRequired().HasMaxLength(20);
        }
    }
}
