using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using AttendanceTrackingSystem.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Reflection;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AttendanceTrackingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepo accountRepo;

        public AccountController(IAccountRepo _accountRepo)
        {
            accountRepo = _accountRepo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = accountRepo.AuthenticateUser(login);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Incorrect Email or Password");
                return View(login);
            }
            else if (!string.IsNullOrEmpty(user.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, user.ErrorMessage);
                return View(login);
            }

            Claim c1 = new Claim(ClaimTypes.NameIdentifier, user.Email);
            Claim c2 = new Claim(ClaimTypes.Name, user.Name);
            Claim c3 = new Claim(ClaimTypes.Role, user.Role);
            Claim c4 = new Claim("UserId", user.Id.ToString());
            ClaimsIdentity claims = new ClaimsIdentity([c1, c2, c3, c4], "cookie");
            ClaimsPrincipal principal = new ClaimsPrincipal(claims);
            await HttpContext.SignInAsync(principal);

            switch (user.Role)
            {
                case "Student":
                    return RedirectToAction("Index", "Student", new { id = user.Id });
                case "Instructor":
                    return RedirectToAction("Index", "Instructor", new { id = user.Id });
                case "Employee":
                    return RedirectToAction("Index", "Employee", new { id = user.Id });
                case "Supervisor":
                    return RedirectToAction("Index", "Supervisor", new { id = user.Id });
                case "Security":
                    return RedirectToAction("Index", "Security", new { id = user.Id });
                case "Admin":
                    return RedirectToAction("Index", "Admin", new { id = user.Id });
                case "StudentAffairs":
                    return RedirectToAction("Index", "StudentAffairs", new { id = user.Id });
                default:
                    return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ResetPasswordEmail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPasswordEmail(ResetPasswordEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = accountRepo.GetUserByEmail(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email is not exist");
                return View(model);
            }

            return RedirectToAction("ResetPassword", new { email = model.Email });
        }

        public IActionResult ResetPassword(string email)
        {
            var model = new ResetPasswordViewModel { Email = email };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(model);
            }

            accountRepo.ChangePassword(model.Email, model.NewPassword);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Profile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = accountRepo.GetUserByEmail(userEmail);
            if (user == null)
            {
                return NotFound();
            }

            switch (user.Role)
            {
                case Role.Student:
                    var student = accountRepo.GetStudentById(user.Id);
                    if (student != null)
                    {
                        return RedirectToAction("StudentProfile", student);
                    }
                    break;
                case Role.Instructor:
                    var instructor = accountRepo.GetInstructorById(user.Id);
                    if (instructor != null)
                    {
                        return RedirectToAction("InstructorProfile", new { id = user.Id });
                    }
                    break;
                default:
                    return RedirectToAction("UserProfile", new { id = user.Id });
            }
            return RedirectToAction("UserProfile", new { id = user.Id });
        }

        public IActionResult StudentProfile(Student student)
        {
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public IActionResult InstructorProfile(Instructor instructor)
        {
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        public IActionResult UserProfile(User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateStudentProfile(int id )
        {
            var student = accountRepo.GetStudentById(id);
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            if (student == null)
            {
                return NotFound();
            }            
            accountRepo.UpdateStudent(student);
            return RedirectToAction("Index", "Student", new { id = student.Id });
        }

        [HttpPost]
        public IActionResult UpdateInstructorProfile(int id)
        {
            var instructor = accountRepo.GetInstructorById(id);
            if (!ModelState.IsValid)
            {
                return View(instructor);
            }
            if (instructor == null)
            {
                return NotFound();
            }
            accountRepo.UpdateInstructor(instructor);
            return RedirectToAction("Index", "Instructor", new { id = instructor.Id });
        }

        [HttpPost]
        public IActionResult UpdateUserProfile(int id)
        {
            var user = accountRepo.GetUserById(id);
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (user == null)
            {
                return NotFound();
            }
            accountRepo.UpdateUser(user);
            return RedirectToAction("Index", "Instructor", new { id = user.Id });
        }

        private static void CommonPropertiesToBeChanged(User user, User target)
        {
            target.Fname = user.Fname;
            target.Lname = user.Lname;
            target.Email = user.Email;
            target.Mobile = user.Mobile;
        }
    }
}
