using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {
    
    sealed class ProfessorModelBuilder : IEntityTypeConfiguration<Professor> {
        public void Configure(EntityTypeBuilder<Professor> builder) {

            builder.ToTable("tb_professor", schema : "Professor");

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