﻿// <auto-generated />
using System;
using AttendanceTrackingSystem.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AttendanceTrackingSystem.Migrations
{
    [DbContext(typeof(ITIDBContext))]
    partial class ITIDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsPermission")
                        .HasColumnType("bit");

                    b.Property<int?>("PermissionType")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeOut")
                        .HasColumnType("datetime2");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("AttendanceID");

                    b.HasIndex("userId");

                    b.ToTable("attendances");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Intake", b =>
                {
                    b.Property<int>("IntakeNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IntakeNumber"));

                    b.Property<string>("IntakeName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.HasKey("IntakeNumber");

                    b.HasIndex("ProgramId");

                    b.ToTable("intakes");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.PermissionRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.HasKey("RequestID");

                    b.HasIndex("studentId");

                    b.ToTable("permissionRequests");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackId"));

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TrackName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("programId")
                        .HasColumnType("int");

                    b.Property<int>("supervisorId")
                        .HasColumnType("int");

                    b.HasKey("TrackId");

                    b.HasIndex("programId");

                    b.HasIndex("supervisorId")
                        .IsUnique();

                    b.ToTable("tracks");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("programs");
                });

            modelBuilder.Entity("InstructorTrack", b =>
                {
                    b.Property<int>("InstructorsId")
                        .HasColumnType("int");

                    b.Property<int>("TracksTrackId")
                        .HasColumnType("int");

                    b.HasKey("InstructorsId", "TracksTrackId");

                    b.HasIndex("TracksTrackId");

                    b.ToTable("InstructorTrack");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Admin", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.User");

                    b.ToTable("admins");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Employee", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.User");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18, 2)");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Instructor", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.User");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18, 2)");

                    b.ToTable("instructors");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Student", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.User");

                    b.Property<string>("Faculty")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("GraduationYear")
                        .HasColumnType("int");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("trackId")
                        .HasColumnType("int");

                    b.HasIndex("trackId");

                    b.ToTable("students");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Security", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.Employee");

                    b.ToTable("security");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.StudentAffairs", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.Employee");

                    b.ToTable("studentAffairs");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Attendance", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.User", "User")
                        .WithMany("Attendances")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Intake", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.program", "program")
                        .WithMany("Intakes")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("program");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.PermissionRequest", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.Student", "Student")
                        .WithMany("PermissionRequests")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Track", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.program", "program")
                        .WithMany("Tracks")
                        .HasForeignKey("programId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceTrackingSystem.Models.Instructor", "Instructor")
                        .WithOne("Track")
                        .HasForeignKey("AttendanceTrackingSystem.Models.Track", "supervisorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("program");
                });

            modelBuilder.Entity("InstructorTrack", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.Instructor", null)
                        .WithMany()
                        .HasForeignKey("InstructorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceTrackingSystem.Models.Track", null)
                        .WithMany()
                        .HasForeignKey("TracksTrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Admin", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.User", null)
                        .WithOne()
                        .HasForeignKey("AttendanceTrackingSystem.Models.Admin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Employee", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.User", null)
                        .WithOne()
                        .HasForeignKey("AttendanceTrackingSystem.Models.Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Instructor", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.User", null)
                        .WithOne()
                        .HasForeignKey("AttendanceTrackingSystem.Models.Instructor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Student", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.User", null)
                        .WithOne()
                        .HasForeignKey("AttendanceTrackingSystem.Models.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceTrackingSystem.Models.Track", "Track")
                        .WithMany("Students")
                        .HasForeignKey("trackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Security", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.Employee", null)
                        .WithOne()
                        .HasForeignKey("AttendanceTrackingSystem.Models.Security", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.StudentAffairs", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.Employee", null)
                        .WithOne()
                        .HasForeignKey("AttendanceTrackingSystem.Models.StudentAffairs", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Track", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.User", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.program", b =>
                {
                    b.Navigation("Intakes");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Instructor", b =>
                {
                    b.Navigation("Track");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Student", b =>
                {
                    b.Navigation("PermissionRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
