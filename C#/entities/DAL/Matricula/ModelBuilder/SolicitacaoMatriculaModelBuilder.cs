using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {
    
    sealed class SolicitacaoMatriculaModelBuilder : IEntityTypeConfiguration<SolicitacaoMatricula> {
        public void Configure(EntityTypeBuilder<SolicitacaoMatricula> builder) {

            builder.ToTable("tb_solicitacao_matricula", schema : "Matricula");

            builder.HasKey(model => model.id);
            builder.Property(c => c.id).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Aluno)
                   .WithMany()
                   .HasForeignKey(c => c.idAluno)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Coordenador)
                   .WithMany()
                   .HasForeignKey(c => c.idCoordenador)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.DisciplinaOfertada)
                   .WithMany()
                   .HasForeignKey(c => c.idDisciplinaOfertada)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.StatusSolicitacaoMatricula)
                   .WithMany()
                   .HasForeignKey(c => c.idStatusSolicitacaoMatricula)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasIndex(
                c => new {
                    c.idAluno, 
                    c.idDisciplinaOfertada
                }
            ).IsUnique();
        }
    }

}