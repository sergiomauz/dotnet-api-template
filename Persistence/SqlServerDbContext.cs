using Microsoft.EntityFrameworkCore;
using Persistence.Mapping;
using Domain.Entities;


namespace Persistence
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = new StudentMap(modelBuilder.Entity<Student>());
            _ = new TeacherMap(modelBuilder.Entity<Teacher>());
            _ = new CourseMap(modelBuilder.Entity<Course>());
            _ = new EnrollmentMap(modelBuilder.Entity<Enrollment>());
        }
    }
}
