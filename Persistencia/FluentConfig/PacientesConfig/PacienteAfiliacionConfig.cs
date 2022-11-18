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
    public class PacienteAfiliacionConfig
    {
        public PacienteAfiliacionConfig(EntityTypeBuilder<PacienteAfiliacionRequest> entity)
        {
            entity.ToTable("PacienteAfiliacion")
                .HasKey(p => p.Id);

             entity
                .HasOne(p => p.Paciente)
                .WithMany(p => p.PacienteAfiliacion)
                .HasForeignKey(p => p.PacienteId)
                .HasConstraintName("FK_Pacientes_Afiliacion")
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.DtFechaRegistro).IsRequired().HasMaxLength(20);
            entity.Property(p => p.UsuarioId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.RegimenId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.AseguradoraId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.VcOtraAseguradora).IsRequired(false).HasMaxLength(20);
        }
    }
}
