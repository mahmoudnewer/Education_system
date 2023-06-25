using Microsoft.EntityFrameworkCore;
using System;

namespace Education.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        DbSet<Instructor> instructors { get; set; }
        DbSet<Student> students { get; set; }
        DbSet<Grade> grades { get; set; }
        DbSet<Role> roles { get; set; }
        DbSet<StudentRequests> studentRequests { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<NewStudentData> newStudentDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                .Property(g => g.grade)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Instructor_Student>()
                .HasKey(e => new { e.Student_Id, e.Instructor_Id });

            base.OnModelCreating(modelBuilder);
        }



    }
}

