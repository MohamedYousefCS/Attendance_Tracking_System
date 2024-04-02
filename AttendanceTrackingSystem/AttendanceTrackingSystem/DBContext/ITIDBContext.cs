using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace AttendanceTrackingSystem.DBContext
{
    public class ITIDBContext : DbContext
    {

        public DbSet<User> users { get; set; }
        public DbSet <Employee> employees { get; set; }
        public DbSet <Admin> admins { get; set; }
        public DbSet <Instructor> instructors { get; set; }
        public DbSet<Student> students {  get; set; }
        public DbSet <Security> security { get; set; }
        public DbSet <StudentAffairs> studentAffairs { get; set; }
        public DbSet <Attendance> attendances { get; set; }
        public DbSet <Intake> intakes { get; set; }
        public DbSet <Track> tracks { get; set; }
        public DbSet <program> programs { get; set; }
        public DbSet <PermissionRequest> permissionRequests { get; set; }
        public DbSet <AttendancePermission> attendancePermissions { get; set; }
       


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("con1");
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(p => { p.UseTptMappingStrategy(); });
            modelBuilder.Entity<Employee>(e => { e.UseTptMappingStrategy(); });
            // Define the relationship between Instructor and Track
            modelBuilder.Entity<Track>()
                .HasOne(t => t.Instructor)
                .WithMany(i => i.Tracks)
                .HasForeignKey(t => t.supervisorId);
            ///////
            modelBuilder.Entity<AttendancePermission>()
      .HasKey(ap => new { ap.AttendanceID, ap.RequestID });

            base.OnModelCreating(modelBuilder);
        }


    }
}
