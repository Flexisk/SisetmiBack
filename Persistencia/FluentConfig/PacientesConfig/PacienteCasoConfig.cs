using Dominio.Pacientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.FluentConfig.PacientesConfig
{
    public class PacienteCasoConfig
    {
        public PacienteCasoConfig(EntityTypeBuilder<PacienteCaso> entity)
        {
            entity.ToTable("PacienteCaso");
            entity.HasKey(p => p.Id);

            entity
               .HasOne(p => p.Pacientes)
               .WithMany(p => p.PacienteCaso)
               .HasForeignKey(p => p.PacienteId)
               .HasConstraintName("FK_Pacientes_Caso")
               .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.VcNumeroCaso).IsRequired().HasMaxLength(20);
            entity.Property(p => p.BComfirmadoSifilisGestacional).IsRequired().HasMaxLength(20);
            entity.Property(p => p.ClasificacionSifilis).IsRequired(false).HasMaxLength(20);
            entity.Property(p => p.DtFecha).IsRequired().HasMaxLength(20);

        }
    }
}
