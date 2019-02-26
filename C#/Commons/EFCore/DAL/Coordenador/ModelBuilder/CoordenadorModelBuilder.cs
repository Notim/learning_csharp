using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {
    
    sealed class CoordenadorModelBuilder : IEntityTypeConfiguration<Coordenador> {
        public void Configure(EntityTypeBuilder<Coordenador> builder) {

            builder.ToTable("tb_coordenador", schema : "Coordenador");

            builder.HasKey(model => model.id);
            builder.Property(c => c.id).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Usuario)
                   .WithMany()
                   .HasForeignKey(c => c.idUsuario)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.email).IsUnique();
            builder.HasIndex(c => c.celular).IsUnique();
        }
    }

}