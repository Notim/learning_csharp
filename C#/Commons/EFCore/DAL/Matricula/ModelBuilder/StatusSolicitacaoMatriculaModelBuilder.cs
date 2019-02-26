using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {
    
    sealed class StatusSolicitacaoMatriculaModelBuilder : IEntityTypeConfiguration<StatusSolicitacaoMatricula> {
        public void Configure(EntityTypeBuilder<StatusSolicitacaoMatricula> builder) {

            builder.ToTable("tb_status_solicitacao_matricula", schema : "Matricula");

            builder.HasKey(model => model.id);
            
            builder.Property(c => c.id).ValueGeneratedOnAdd();

            builder.Property(c => c.descricao).HasMaxLength(20);
        }
    }

}