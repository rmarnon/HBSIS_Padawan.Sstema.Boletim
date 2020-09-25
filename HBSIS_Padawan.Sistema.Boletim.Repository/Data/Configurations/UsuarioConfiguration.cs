using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS_Padawan.Sistema.Boletim.Repository.Data.Configurations
{
    class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Login).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Senha).IsRequired();
        }
    }
}
