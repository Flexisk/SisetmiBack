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
    public class PacienteDiagnosticoConfig
    {
        public PacienteDiagnosticoConfig(EntityTypeBuilder<PacienteDiagnostico> entity)
        {
            entity.ToTable("PacienteDiagnostico");
            entity.HasKey(p => p.Id);

            entity
                .HasOne(p => p.PacienteCaso)
                .WithMany(p => p.PacienteDiagnostico)
                .HasForeignKey(p => p.PacienteCasoId)
                .HasConstraintName("FK_Paciente_Diagnostico")
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.PacienteCasoId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.DtFechaDiagnostico).IsRequired();
            entity.Property(p => p.IpsDiagnosticaId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.FormulaObstetricaId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.CondicionActualId).IsRequired().HasMaxLength(200);
            entity.Property(p => p.VcEdadGestacional).IsRequired(false).HasMaxLength(200);
            entity.Property(p => p.AntecedenteItsId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.TipoAntecedenteId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.VcDescripcionAntecedente).IsRequired(false).HasMaxLength(200);
            entity.Property(p => p.AntecedentePenicilinaId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.BRequierePruebaNoTreponemica).IsRequired();
            entity.Property(p => p.TipoPruebaNoTriponemica).IsRequired().HasMaxLength(20);
            entity.Property(p => p.ResultadoPruebaNoTriponemica).IsRequired(false);
            entity.Property(p => p.DtResultadoPruebaNoTreponemica).IsRequired().HasMaxLength(20);
            entity.Property(P => P.ModificacionDefinicionCasoId).IsRequired().HasMaxLength(20);

        }
    }
}
