using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace entities.core.Entities {

    public class Classroom {
        public int    id   { get; set; }
        public string desc { get; set; }
    }

    sealed class ClassroomModelBuilder : IEntityTypeConfiguration<Classroom> {
        public void Configure(EntityTypeBuilder<Classroom> builder){

            builder.ToTable("tb_classroom");
            builder.HasKey(model => model.id);
            builder.Property(c => c.id).ValueGeneratedOnAdd();
            
            /*
            builder.HasOne(c => c.])
                   .WithOne(c => c.File)
                   .HasForeignKey<Message>(c => c.MessageId)
                   .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }

}