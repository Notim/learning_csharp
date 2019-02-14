using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {

    sealed class CursoModelBuilder : IEntityTypeConfiguration<Curso> {
        public void Configure(EntityTypeBuilder<Curso> builder) {
            builder.ToTable("tb_curso", schema: "Curso");

            builder.HasKey(model => model.id);
            builder.Property(c => c.id).ValueGeneratedOnAdd();

            builder.HasIndex(c => c.nome).IsUnique();
        }
    }

}