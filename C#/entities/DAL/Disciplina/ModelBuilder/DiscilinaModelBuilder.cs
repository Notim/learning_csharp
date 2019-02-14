using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {
    
    sealed class DisciplinaModelBuilder : IEntityTypeConfiguration<Disciplina> {
        public void Configure(EntityTypeBuilder<Disciplina> builder) {

            builder.ToTable("tb_disciplina", schema : "Disciplina");

            builder.HasKey(model => model.id);
            builder.Property(c => c.id).ValueGeneratedOnAdd();

            builder.Property(c => c.nome).HasMaxLength(50);
            builder.Property(c => c.nome).HasMaxLength(50);
            builder.Property(c => c.planoEnsino).HasMaxLength(200);
            builder.Property(c => c.competencias).HasMaxLength(500);
            builder.Property(c => c.habilidades).HasMaxLength(100);
            builder.Property(c => c.emenda).HasMaxLength(100);
            builder.Property(c => c.conteudoProgramatico).HasMaxLength(100);
            builder.Property(c => c.bibliografiaBasica).HasMaxLength(100);
            builder.Property(c => c.bibliografiaComplementar).HasMaxLength(100);
            builder.Property(c => c.conteudoProgramatico).HasMaxLength(10);
            builder.Property(c => c.percentualPratico).HasMaxLength(10);
            builder.Property(c => c.percentualTeorico).HasMaxLength(10);
            
            builder.HasOne(c => c.StatusDisciplina)
                   .WithMany()
                   .HasForeignKey(c => c.idStatusDisciplina)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Coordenador)
                   .WithMany()
                   .HasForeignKey(c => c.idCoordenador)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasIndex(c => c.nome).IsUnique();
            
        }
    }

}