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
    public class PacienteContactoConfig
    {
        public PacienteContactoConfig(EntityTypeBuilder<PacienteContacto> entity)
        {
            entity.ToTable("PacienteContacto")
                .HasKey(p => p.Id);

            entity
                .HasOne(p => p.Paciente)
                .WithMany(p => p.PacienteContacto)
                .HasForeignKey(p => p.PacienteId)
                .HasConstraintName("FK_Paciente_Contacto")
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.DtFechaRegistro).IsRequired().HasMaxLength(20);
            entity.Property(p => p.UsuarioId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.PaisId).IsRequired();
            entity.Property(p => p.DepartamentoId).IsRequired().HasMaxLength(100);
            entity.Property(p => p.LocalidadId).IsRequired().HasMaxLength(100);
            entity.Property(p => p.BarrioId).IsRequired().HasMaxLength(50);
            entity.Property(p => p.VcDireccionPrincipal).IsRequired().HasMaxLength(200);
            entity.Property(p => p.VcDireccionSecundaria).IsRequired(false).HasMaxLength(200);
            entity.Property(p => p.VcTelefono1).IsRequired().HasMaxLength(20);
            entity.Property(p => p.VcTelefono2).IsRequired(false).HasMaxLength(20);

        }
    }
}
