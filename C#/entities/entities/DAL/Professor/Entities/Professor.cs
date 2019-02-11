using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities {

    public class Professor {
        public int    id        { get; set; }
        public int    idUsuario { get; set; }
        public string nome      { get; set; }
        public string email     { get; set; }
        public string celular   { get; set; }
        public string apelido { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    sealed class ProfessorModelBuilder : IEntityTypeConfiguration<Professor> {
        public void Configure(EntityTypeBuilder<Professor> builder) {

            builder.ToTable("tb_professor");

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