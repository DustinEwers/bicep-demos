using Demo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Data
{
    public class ExerciseAppContext: DbContext
    {
        public ExerciseAppContext(DbContextOptions<ExerciseAppContext> options) : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagAssignment> TagAssignments { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
    }
}
