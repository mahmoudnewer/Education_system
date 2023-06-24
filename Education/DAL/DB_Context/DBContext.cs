using Education.DAL.models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Education.DAL.DB_Context
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        DbSet<Instructor>instructors  { get; set; }
        DbSet<Student> students { get; set; }
        DbSet<Grade> grades { get; set; }
        DbSet<Role> roles { get; set; }
        DbSet<StudentRequests> studentRequests { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<NewStudentData> newStudentDatas { get; set; }



    }
}

