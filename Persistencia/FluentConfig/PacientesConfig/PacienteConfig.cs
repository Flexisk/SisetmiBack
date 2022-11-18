using Dominio.Paciente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistencia.FluentConfig.PacientesConfig
{
    public class PacienteConfig
    {
        public PacienteConfig(EntityTypeBuilder<PacienteRequest> entity)
        {
            entity.ToTable("Paciente");
            entity.HasKey(p => p.Id);
          
            entity.Property(p => p.TipoDocumentoId).IsRequired().HasMaxLength(20);
            entity.Property(p => p.VcDocumento).IsRequired().HasMaxLength(100);
            entity.Property(p => p.VcPrimerNombre).IsRequired().HasMaxLength(150);
            entity.Property(p => p.VcSegundoNombre).IsRequired(false).HasMaxLength(150);
            entity.Property(p => p.VcPrimerApellido).IsRequired().HasMaxLength(150);
            entity.Property(p => p.VcSegundoApellido).IsRequired(false).HasMaxLength(150);
            entity.Property(p => p.Nacionalidad).IsRequired().HasMaxLength(100);
            entity.Property(p => p.DtFechaNacimineto).IsRequired().HasMaxLength(50);

        }
   
    }
}   
