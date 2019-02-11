using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities {

    public class Usuario {
        public int      id          { get; set; }
        public string   login       { get; set; }
        public string   senha       { get; set; }
        public DateTime dtExpiracao { get; set; }
    }

    sealed class UsuarioModelBuilder : IEntityTypeConfiguration<Usuario> {
        public void Configure(EntityTypeBuilder<Usuario> builder) {
            
            builder.ToTable("tb_usuario");

            builder.HasKey(model => model.id);

            builder.Property(c => c.id)
                   .ValueGeneratedOnAdd();

            /*builder.HasIndex(model => model.login)
                   .IsUnique();*/
        }
    }

}