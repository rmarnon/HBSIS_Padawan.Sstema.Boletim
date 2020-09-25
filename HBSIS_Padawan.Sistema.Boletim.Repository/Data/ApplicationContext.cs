using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HBSIS_Padawan.Sistema.Boletim.Repository.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CursoMateria> Disciplinas { get; set; }
        public DbSet<CursoMateriaAluno> AlunoNotas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Data Source=NT-04822\SQLEXPRESS;Initial Catalog=SistemaUniversitario;Integrated Security=true;MultipleActiveResultSets=true",
                p => p.EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null)
                .MigrationsHistoryTable("Boletim_Migrations_History"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
