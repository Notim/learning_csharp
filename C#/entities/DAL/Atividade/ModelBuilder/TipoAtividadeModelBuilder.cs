using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {

    internal sealed class TipoAtividadeModelBuilder : IEntityTypeConfiguration<TipoAtividade> {
        public void Configure(EntityTypeBuilder<TipoAtividade> builder) {
            builder.ToTable("tb_atividade", schema: "Atividade");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id).ValueGeneratedOnAdd();
            builder.Property(c => c.descricao).HasMaxLength(40);
    
            builder.HasIndex(c => c.descricao).IsUnique();
            
        }
    }

}