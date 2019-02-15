using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {

    internal sealed class AlunoModelBuilder : IEntityTypeConfiguration<Aluno> {
        public void Configure(EntityTypeBuilder<Aluno> builder) {
            builder.ToTable("tb_aluno", schema: "Aluno");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id).ValueGeneratedOnAdd();
            
            builder.HasIndex(c => c.email).IsUnique();
            builder.HasIndex(c => c.celular).IsUnique();
            
            builder.HasOne(c => c.Usuario)
                   .WithMany()
                   .HasForeignKey(c => c.idUsuario)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}