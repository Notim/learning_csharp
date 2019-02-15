using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {

    internal sealed class AtividadeEntregaModelBuilder : IEntityTypeConfiguration<AtividadeEntrega> {
        public void Configure(EntityTypeBuilder<AtividadeEntrega> builder) {
            
            builder.ToTable("tb_atividade_entrega", schema: "Atividade");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id).ValueGeneratedOnAdd();
            builder.Property(c => c.titulo).HasMaxLength(20);

            builder.HasIndex(
                c => new {
                    c.idAluno, 
                    c.idAtividadeVinculada
                }
            ).IsUnique();
            
            builder.HasOne(c => c.Aluno)
                   .WithMany()
                   .HasForeignKey(c => c.idAluno)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Professor)
                   .WithMany()
                   .HasForeignKey(c => c.idProfessor)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.StatusEntregaAtividade)
                   .WithMany()
                   .HasForeignKey(c => c.idStatusEntregaAtividade)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.AtividadeVinculada)
                   .WithMany()
                   .HasForeignKey(c => c.AtividadeVinculada)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
