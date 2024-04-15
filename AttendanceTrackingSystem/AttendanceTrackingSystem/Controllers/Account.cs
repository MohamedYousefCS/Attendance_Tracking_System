using AttendanceTrackingSystem.Repos;
using AttendanceTrackingSystem.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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
            ClaimsIdentity claims = new ClaimsIdentity(new[] { c1, c2, c3 }, "cookie");
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

        //public IActionResult Profile()
        //{
        //    var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = accountRepo.GetUserByEmail(userEmail);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    var viewModel = new UserViewModel
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        Name = user.Name,
        //        Role = user.Role,
        //        // Add other properties as needed
        //    };

        //    return View(viewModel);
        //}

        //[HttpPost]
        //public IActionResult Profile(UserViewModel model)
        //{
        //    // Check if the model is valid
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model); // Return the same view with validation errors
        //    }

        //    // Retrieve the currently logged-in user's information
        //    var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = accountRepo.GetUserByEmail(userEmail);

        //    // Check if the user exists
        //    if (user == null)
        //    {
        //        return NotFound(); // Or return an appropriate error view
        //    }

        //    // Update the user's data with the data from the submitted form
        //    user.Name = model.Name;
        //    // Update other properties as needed

        //    // Save the changes to the user's data
        //    accountRepo.UpdateUser(user);

        //    // Redirect to a confirmation page or refresh the profile page
        //    return RedirectToAction(nameof(Profile));
        //}

    }
}
