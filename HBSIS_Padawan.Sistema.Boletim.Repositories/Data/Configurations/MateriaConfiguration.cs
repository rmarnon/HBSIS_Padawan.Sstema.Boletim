using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS_Padawan.Sistema.Boletim.Repositories.Data.Configurations
{
    public class MateriaConfiguration : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.ToTable("Materias");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasColumnType("Varchar(50)").IsRequired();
            builder.Property(p => p.Cadastro).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(p => p.Descricao).HasColumnType("TEXT").IsRequired();
            builder.Property(p => p.Status).HasConversion<string>().HasDefaultValue(Status.Ativo);
        }
    }
}
