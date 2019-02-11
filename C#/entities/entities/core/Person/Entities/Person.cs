using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace entities.core.Entities {

    public class Person {
        public int id { get; set; }
        
        public string name { get; set; }
        
        public DateTime? birthday { get; set; }
        
        public Classroom Classroom { get; set; }
        
        public int? idClassroom { get; set; }
    }

    sealed class PersonModelBuilder : IEntityTypeConfiguration<Person> {
        public void Configure(EntityTypeBuilder<Person> builder) {

            builder.ToTable("tb_person");
            
            builder.HasKey(model => model.id);
            
            builder.Property(c => c.id).ValueGeneratedOnAdd();
            
            builder.HasOne(c => c.Classroom)
                   .WithMany()
                   .HasForeignKey(c => c.idClassroom)
                   .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}