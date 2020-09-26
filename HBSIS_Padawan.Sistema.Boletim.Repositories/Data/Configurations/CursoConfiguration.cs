using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS_Padawan.Sistema.Boletim.Repositories.Data.Configurations
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasColumnType("Varchar(50)").IsRequired();
            builder.Property(p => p.Situacao).HasConversion<string>().HasDefaultValue(Status.Ativo);
        }
    }
}
