using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities {

    public class Coordenador : DefaultEntity {
        public int    id        { get; set; }
        public int    idUsuario { get; set; }
        public string nome      { get; set; }
        public string email     { get; set; }
        public string celular   { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    sealed class CoordenadorModelBuilder : IEntityTypeConfiguration<Coordenador> {
        public void Configure(EntityTypeBuilder<Coordenador> builder) {

            builder.ToTable("tb_coordenador");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(c => c.Usuario)
                   .WithMany()
                   .HasForeignKey(c => c.idUsuario)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.email)
                   .IsUnique();

            builder.HasIndex(c => c.celular)
                   .IsUnique();
        }
    }

}