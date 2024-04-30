using AttendanceTrackingSystem.Repos;
using AttendanceTrackingSystem.Models;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly IAdmin adminRepo;

        public AdminController(IUserRepo _userRepo, IAdmin _adminRepo)
        {
            userRepo = _userRepo;
            adminRepo = _adminRepo;
        }
        public IActionResult Index()
        {
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            User admin=adminRepo.GetAdminByID(userId);
            return View(admin);
        }
    }
}
