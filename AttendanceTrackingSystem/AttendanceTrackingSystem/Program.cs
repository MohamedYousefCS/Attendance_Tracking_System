using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Repos;
using OfficeOpenXml;

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
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
                pattern: "{controller=Student}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
