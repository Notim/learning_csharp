using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {

    internal sealed class AtividadeVinculadaModelBuilder : IEntityTypeConfiguration<AtividadeVinculada> {
        public void Configure(EntityTypeBuilder<AtividadeVinculada> builder) {
            
            builder.ToTable("tb_atividade_vinculada", schema: "Atividade");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id).ValueGeneratedOnAdd();
            
            builder.Property(c => c.rotulo).HasMaxLength(50);
  
            builder.HasIndex(
               c => new {
                  c.rotulo,
                  c.idAtividade,
                  c.idDisciplinaOfertada
               }
            ).IsUnique();
            
            builder.HasOne(c => c.Professor)
                   .WithMany()
                   .HasForeignKey(c => c.idProfessor)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.StatusAtividade)
                   .WithMany()
                   .HasForeignKey(c => c.StatusAtividade)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Atividade)
                   .WithMany()
                   .HasForeignKey(c => c.idAtividade)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.DisciplinaOfertada)
                   .WithMany()
                   .HasForeignKey(c => c.idDisciplinaOfertada)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }

}