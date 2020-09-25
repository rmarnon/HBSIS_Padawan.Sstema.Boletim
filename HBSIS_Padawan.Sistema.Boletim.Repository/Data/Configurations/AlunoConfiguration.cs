using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Padawan.SistemaUniversitário.Repository.Data.Configurations
{
    class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Sobrenome).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Nascimento).IsRequired();
            builder.Property(p => p.Cpf).IsRequired();

            builder.HasOne(p => p.Curso)
                .WithMany(p => p.Alunos)
                .HasForeignKey(p => p.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.NotasMaterias)
                .WithOne(p => p.Aluno)
                .HasForeignKey(p => p.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
