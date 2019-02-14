using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {
    
    sealed class StatusDisciplinaModelBuilder : IEntityTypeConfiguration<StatusDisciplina> {
        public void Configure(EntityTypeBuilder<StatusDisciplina> builder) {

            builder.ToTable("tb_status_disciplina", schema : "Disciplina");

            builder.HasKey(model => model.id);
            
            builder.Property(c => c.id).ValueGeneratedOnAdd();
            
            builder.Property(c => c.descricao).HasMaxLength(50);

            builder.HasIndex(c => c.descricao).IsUnique();
        }
    }

}