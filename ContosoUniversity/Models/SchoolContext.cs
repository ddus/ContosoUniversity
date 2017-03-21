using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Models
{
  public class Student
  {
    [Key]
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public byte[] Rights { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
  }

  public class StudentWithTag : Student
  {
    public dynamic tag;

    public StudentWithTag(Student student)
    {
      this.ID = student.ID;
      this.LastName = student.LastName;
      this.FirstMidName = student.FirstMidName;
      this.EnrollmentDate = student.EnrollmentDate;
      this.Enrollments = student.Enrollments;
    }
  }

  public enum Grade
  {
    A, B, C, D, F
  }

  public class Enrollment
  {
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public Course Course { get; set; }
    public Student Student { get; set; }
  }

  public class Course
  {
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CourseID { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
  }

  public class SchoolContext : DbContext
  {
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Course>().ToTable("Course");
      modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
      modelBuilder.Entity<Student>().ToTable("Student");
    }

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }
  }
}
