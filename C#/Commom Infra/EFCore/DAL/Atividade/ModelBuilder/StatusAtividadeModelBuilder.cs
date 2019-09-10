using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {

    internal sealed class StatusAtividadeModelBuilder : IEntityTypeConfiguration<StatusAtividade> {
        public void Configure(EntityTypeBuilder<StatusAtividade> builder) {
            builder.ToTable("tb_atividade_status", schema: "Atividade");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id).ValueGeneratedOnAdd();
            builder.Property(c => c.descricao).HasMaxLength(40);
    
            builder.HasIndex(c => c.descricao).IsUnique();
            
        }
    }

}