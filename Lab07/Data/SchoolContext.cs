using Lab07.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab07.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Course> Courses => Set<Course>();
}
