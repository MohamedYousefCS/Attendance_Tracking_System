﻿using AttendanceTrackingSystem.Repos;
using AttendanceTrackingSystem.Models;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
    public class AdminController : Controller
    {
        public IAdmin adminRepo;

        public AdminController(IAdmin adminRepo)
        {
            this.adminRepo = adminRepo;
        }
        public IActionResult Index()
        {
            //int userId = int.Parse(User.FindFirstValue("UserId"));

          User admin=adminRepo.GetAdminByID(64);
            return View(admin);
        }
    }
}