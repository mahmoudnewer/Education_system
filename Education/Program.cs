using Education.Models;
using Education.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Education
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("EducationConnectionString"));
            });


            builder.Services.AddScoped<IGenericRepository<Grade>, GenericRepository<Grade>>();
            builder.Services.AddScoped<IGenericRepository<Instructor>, GenericRepository<Instructor>>();
            builder.Services.AddScoped<IGenericRepository<Instructor_Student>, GenericRepository<Instructor_Student>>();
            builder.Services.AddScoped<IGenericRepository<NewStudentData>, GenericRepository<NewStudentData>>();
            builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
            builder.Services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            builder.Services.AddScoped<IGenericRepository<StudentRequests>, GenericRepository<StudentRequests>>();
            builder.Services.AddScoped<IGenericRepository<Topic>, GenericRepository<Topic>>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}