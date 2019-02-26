using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {

    internal sealed class AtividadeModelBuilder : IEntityTypeConfiguration<Atividade> {
        public void Configure(EntityTypeBuilder<Atividade> builder) {
            
            builder.ToTable("tb_atividade", schema: "Atividade");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id).ValueGeneratedOnAdd();
            builder.Property(c => c.titulo).HasMaxLength(30);
            builder.Property(c => c.descricao).HasMaxLength(100);

            builder.HasIndex(c => c.titulo).IsUnique();
            
            builder.HasOne(c => c.TipoAtividade)
                   .WithMany()
                   .HasForeignKey(c => c.idTipoAtividade)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Professor)
                   .WithMany()
                   .HasForeignKey(c => c.idProfessor)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }

}