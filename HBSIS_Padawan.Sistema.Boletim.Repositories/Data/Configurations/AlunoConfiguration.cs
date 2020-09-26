using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS_Padawan.Sistema.Boletim.Repositories.Data.Configurations
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Sobrenome).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Nascimento).IsRequired();
            builder.Property(p => p.Cpf).IsRequired();            
        }
    }
}
