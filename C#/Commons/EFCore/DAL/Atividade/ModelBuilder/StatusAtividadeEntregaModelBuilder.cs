using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {

    internal sealed class StatusAtividadeEntregaModelBuilder : IEntityTypeConfiguration<StatusAtividadeEntrega> {
        public void Configure(EntityTypeBuilder<StatusAtividadeEntrega> builder) {
            builder.ToTable("tb_atividade_status_entrega", schema: "Atividade");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id).ValueGeneratedOnAdd();
            builder.Property(c => c.descricao).HasMaxLength(40);
    
            builder.HasIndex(c => c.descricao).IsUnique();
            
        }
    }

}