using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration {

    public partial class DataContext : DbContext {

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            this.MapperModuloCoordenador(modelBuilder);
            this.MapperModuloAluno(modelBuilder);
            this.MapperModuloProfessor(modelBuilder);
            this.MapperModuloUsuario(modelBuilder);
            this.MapperModuloDisciplina(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }

}