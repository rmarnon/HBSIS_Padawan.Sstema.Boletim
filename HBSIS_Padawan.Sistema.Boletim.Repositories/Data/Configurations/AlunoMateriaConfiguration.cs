using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS_Padawan.Sistema.Boletim.Repositories.Data.Configurations
{
    public class AlunoMateriaConfiguration : IEntityTypeConfiguration<AlunoMateria>
    {
        public void Configure(EntityTypeBuilder<AlunoMateria> builder)
        {
            builder.ToTable("AlunoMateria");           
            builder.HasKey(p => new 
            {
                p.AlunoId, p.MateriaId
            });
        }
    }
}
