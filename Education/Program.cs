using AutoMapper;
using Education.helpers;
using Education.Models;
using Education.Repositories;
using Education.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


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
            builder.Services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
           
            builder.Services.AddScoped<IStudentService,StudentService>();
            builder.Services.AddScoped<INewStudentService, NewStudentService>();
 
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<ITopicService, TopicService>();
            builder.Services.AddScoped<IGenericRepository<Grade>, GenericRepository<Grade>>();
            builder.Services.AddScoped<IGenericRepository<Instructor>, GenericRepository<Instructor>>();
            builder.Services.AddScoped<IGenericRepository<Instructor_Student>, GenericRepository<Instructor_Student>>();
            builder.Services.AddScoped<IGenericRepository<NewStudentData>, GenericRepository<NewStudentData>>();
            builder.Services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
            builder.Services.AddScoped<IGenericRepository<StudentRequests>, GenericRepository<StudentRequests>>();
            builder.Services.AddScoped<IGenericRepository<Topic>, GenericRepository<Topic>>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();



            builder.Services.AddScoped<IRequestsServices, RequestsServices>();
            builder.Services.AddScoped<IGradeServices, GradeServices>();


            var config = new MapperConfiguration(cfg =>
            {

                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            var mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);



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