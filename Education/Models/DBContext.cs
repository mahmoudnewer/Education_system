using Microsoft.EntityFrameworkCore;
using System;

namespace Education.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            
        }

        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Grade> grades { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<StudentRequests> studentRequests { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<NewStudentData> newStudentDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                .Property(g => g.grade)
                .HasColumnType("decimal(18, 2)");

            /*modelBuilder.Entity<Instructor>()
               .Property(i => i.image)
               .HasColumnType("varbinary(max)");*/
          
            modelBuilder.Entity<Instructor_Student>()
                .HasKey(e => new { e.Student_Id, e.Instructor_Id });

            base.OnModelCreating(modelBuilder);
        }



    }
}

