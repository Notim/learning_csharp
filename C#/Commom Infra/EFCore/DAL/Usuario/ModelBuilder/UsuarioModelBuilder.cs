using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {

    sealed class UsuarioModelBuilder : IEntityTypeConfiguration<Usuario> {
        public void Configure(EntityTypeBuilder<Usuario> builder) {

            builder.ToTable("tb_usuario", schema : "Usuario");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id)
                   .ValueGeneratedOnAdd();

        }
    }

}