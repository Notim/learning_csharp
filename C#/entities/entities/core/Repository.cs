using entities.core.Entities;

using Microsoft.EntityFrameworkCore;

namespace entities.core {

    public partial class EntitiesCore : DbContext {
        public virtual DbSet<Person> Person { get; set; }

        public virtual DbSet<Classroom> Classrooms { get; set; }

    }

}