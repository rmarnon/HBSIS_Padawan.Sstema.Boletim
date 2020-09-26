using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS_Padawan.Sistema.Boletim.Repositories.Data.Configurations
{
    public class AlunoCursoConfiguration : IEntityTypeConfiguration<AlunoCurso>
    {
        public void Configure(EntityTypeBuilder<AlunoCurso> builder)
        {
            builder.ToTable("AlunoCurso");
            builder.HasKey(p => new
            {
                p.AlunoId, p.CursoId
            });
        }
    }
}
