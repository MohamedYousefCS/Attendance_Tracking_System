using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Repos;

namespace AttendanceTrackingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IStudentRepo, StudentRepo>();
            builder.Services.AddTransient<IStudentAffairsRepo, StudentAffairsRepo>();


            builder.Services.AddDbContext<ITIDBContext>();


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
                pattern: "{controller=StudentAffairs}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
