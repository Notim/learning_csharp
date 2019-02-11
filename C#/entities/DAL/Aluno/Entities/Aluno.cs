using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities {

    public class Aluno : DefaultEntity {
        public int    id        { get; set; }
        public int    idUsuario { get; set; }
        public string nome      { get; set; }
        public string email     { get; set; }
        public string celular   { get; set; }
        public string ra        { get; set; }
        public string foto      { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    internal sealed class AlunoModelBuilder : IEntityTypeConfiguration<Aluno> {
        public void Configure(EntityTypeBuilder<Aluno> builder) {

            builder.ToTable("tb_aluno");

            //modelBuilder.HasDefaultSchema("blogging"); // for sqlServer

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