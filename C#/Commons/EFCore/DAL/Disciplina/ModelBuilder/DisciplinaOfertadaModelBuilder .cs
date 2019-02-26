using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {
    
    sealed class DisciplinaOfertadaModelBuilder : IEntityTypeConfiguration<DisciplinaOfertada> {
        public void Configure(EntityTypeBuilder<DisciplinaOfertada> builder) {

            builder.ToTable("tb_disciplina_ofertada", schema : "Disciplina");

            builder.HasKey(model => model.id);
            builder.Property(c => c.id).ValueGeneratedOnAdd();

            builder.Property(c => c.turma).HasMaxLength(1);
            builder.Property(c => c.metodologia).HasMaxLength(100);
            builder.Property(c => c.recursos).HasMaxLength(100);
            builder.Property(c => c.criterioAvaliacao).HasMaxLength(100);
            builder.Property(c => c.planoAulas).HasMaxLength(500);
            
            builder.HasOne(c => c.Curso)
                   .WithMany()
                   .HasForeignKey(c => c.idCurso)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Professor)
                   .WithMany()
                   .HasForeignKey(c => c.idProfessor)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Disciplina)
                   .WithMany()
                   .HasForeignKey(c => c.idDisciplina)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasIndex(
                c => new {
                    c.idDisciplina, 
                    c.idCurso, 
                    c.ano, 
                    c.semestre, 
                    c.turma
                }
            ).IsUnique();
            
        }
    }

}