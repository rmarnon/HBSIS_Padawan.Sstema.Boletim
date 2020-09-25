using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS_Padawan.Sistema.Boletim.Repository.Data.Configurations
{
    class CursoMateriaConfiguration : IEntityTypeConfiguration<CursoMateria>
    {
        public void Configure(EntityTypeBuilder<CursoMateria> builder)
        {
            builder.ToTable("CursoMateria");
            builder.HasKey(p => new
            {
                p.CursoId, p.MateriaId
            });

            builder.HasMany(p => p.NotasAlunos)
                .WithOne(p => p.CursoMateria)
                .HasForeignKey(p => p.CursoMateriaId);

            builder.HasOne(p => p.Curso)
                .WithMany(p => p.Disciplinas)
                .HasForeignKey(p => p.CursoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
