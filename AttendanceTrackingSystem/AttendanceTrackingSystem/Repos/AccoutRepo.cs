using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AttendanceTrackingSystem.Repos
{
    public interface IAccountRepo
    {
        UserViewModel AuthenticateUser(UserViewModel login);
        void ChangePassword(string email, string newPassword);
        UserViewModel GetUserById(int id);
        User GetUserByEmail(string email);
    }

    public class AccountRepo : IAccountRepo
    {
        private readonly ITIDBContext db;

        public AccountRepo(ITIDBContext _db)
        {
            db = _db;
        }

        public UserViewModel AuthenticateUser(UserViewModel login)
        {
            var user = db.users.FirstOrDefault(u => u.Email == login.Email);

            if (user == null)
            {
                return new UserViewModel { ErrorMessage = "Email does not exist" };
            }

            if (user.Password != login.Password)
            {
                return new UserViewModel { ErrorMessage = "Incorrect password" };
            }

            var name = user.Fname + " " + user.Lname;
            return new UserViewModel { Email = login.Email, Password = login.Password, Role = user.Role.ToString(), Name = name };
        }

        public void ChangePassword(string email, string newPassword)
        {
            var user = db.users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.Password = newPassword;
                db.SaveChanges();
            }
        }

        public UserViewModel GetUserById(int id)
        {
            if (db.users.Any(u => u.Id == id))
            {
                var user = db.users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
                var name = user.Fname + " " + user.Lname;
                return new UserViewModel { Id = id, Role = user.Role.ToString(), Name = name };
            }
            return null;
        }

        public User GetUserByEmail(string email)
        {
            return db.users.FirstOrDefault(u => u.Email == email);
        }
    }
}
