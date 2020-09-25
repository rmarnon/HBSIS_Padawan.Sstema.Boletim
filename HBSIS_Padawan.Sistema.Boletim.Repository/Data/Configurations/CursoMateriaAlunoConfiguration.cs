using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS_Padawan.Sistema.Boletim.Repository.Data.Configurations
{
    class CursoMateriaAlunoConfiguration : IEntityTypeConfiguration<CursoMateriaAluno>
    {
        public void Configure(EntityTypeBuilder<CursoMateriaAluno> builder)
        {
            builder.ToTable("CursoMateriaAluno");
            builder.HasKey(p => new
            {
                p.AlunoId, p.CursoMateriaId
            });

            builder.HasOne(p => p.Aluno).WithMany(p => p.NotasMaterias);   

            builder.HasOne(p => p.CursoMateria)
                .WithMany(p => p.NotasAlunos)  
                .HasForeignKey(p => p.CursoMateriaId)                
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
