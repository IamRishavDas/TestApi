using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // one to many relationship between the course and the instructor
            modelBuilder.Entity<CourseModel>()
                .HasOne(c => c.Instructor) // a course has one instructor
                .WithMany(i => i.Courses)  // one instructor has many courses
                .HasForeignKey(c => c.InstructorId) // fk instructorId in course Model
                .OnDelete(DeleteBehavior.Cascade); // remove to maintain the ref integrity

            // many to many relationship between the student and course
            modelBuilder.Entity<CourseModel>()
                .HasMany(c => c.Students) // a course has many students
                .WithMany(s => s.Courses) // a student can have many courses
                .UsingEntity<CourseStudentModel>(cs => cs
                    .HasOne(cs => cs.Student)
                    .WithMany(s => s.CourseStudents)
                    .HasForeignKey(cs => cs.StudentId),
                     cs => cs
                    .HasOne(cs => cs.Course)
                    .WithMany(c => c.CourseStudents)
                    .HasForeignKey(cs => cs.CourseId)
                 );
        }

        public DbSet<StudentModel> Students { get; set; }
        public DbSet<InstructorModel> Instructors { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<CourseStudentModel> CourseStudents { get; set; }
    }
}
