using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS_Padawan.Sistema.Boletim.Repositories.Data.Configurations
{
    public class CursoMateriaConfiguration : IEntityTypeConfiguration<CursoMateria>
    {
        public void Configure(EntityTypeBuilder<CursoMateria> builder)
        {
            builder.ToTable("CursoMateria");
            builder.HasKey(p => new 
            {
                p.CursoId, p.MateriaId
            });
        }
    }
}
