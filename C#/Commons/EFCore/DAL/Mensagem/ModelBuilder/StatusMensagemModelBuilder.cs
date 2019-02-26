using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {
    
    sealed class StatusMensagemModelBuilder : IEntityTypeConfiguration<StatusMensagem> {
        public void Configure(EntityTypeBuilder<StatusMensagem> builder) {

            builder.ToTable("tb_status_mensagem", schema : "Mensagem");

            builder.HasKey(model => model.id);
            builder.Property(c => c.id).ValueGeneratedOnAdd();

            builder.Property(c => c.descricao).HasMaxLength(50);

        }
    }

}