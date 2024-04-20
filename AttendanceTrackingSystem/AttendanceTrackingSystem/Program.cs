using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            builder.Services.AddTransient<IEmployeeRepo, EmployeeRepo>();


           
            builder.Services.AddScoped<AdTrackRepo>();
            builder.Services.AddScoped<PermissionReqRepo>();
            builder.Services.AddScoped<InsStdAttendanceRepo>();



            builder.Services.AddTransient<IAccountRepo, AccountRepo>();
            
            builder.Services.AddScoped<IInstructorRepo, InstructorRepo>();
            builder.Services.AddTransient<IAttendance,AttendanceRepo>();
            builder.Services.AddTransient<IUserRepo,UserRepo>();
			builder.Services.AddTransient<IAdmin, AdminRepo>();


			builder.Services.AddDbContext<ITIDBContext>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.LoginPath = "/Account/Login";
            });
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
