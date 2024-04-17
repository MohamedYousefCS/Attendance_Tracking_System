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
        User GetUserByEmail(string email);
        User GetUserById(int id);
        public UserViewModel GetUserByIdReturnViewModel(int id);
        Student GetStudentById(int id);
        Instructor GetInstructorById(int id);
        void UpdateUser(User user);
        void UpdateStudent(Student student);
        void UpdateInstructor(Instructor instructor);
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
            return new UserViewModel { Email = login.Email, Password = login.Password, Role = user.Role.ToString(), Name = name, Id = user.Id};
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

        public UserViewModel GetUserByIdReturnViewModel(int id)
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

        public Student GetStudentById(int id)
        {
            return db.students.FirstOrDefault(s => s.Id == id);
        }

        public Instructor GetInstructorById(int id)
        {
            return db.instructors.FirstOrDefault(i => i.Id == id);
        }

        public User GetUserById(int id)
        {
            return db.users.FirstOrDefault(i => i.Id == id);
        }

        public void UpdateUser(User user)
        {
            db.users.Update(user);
            db.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            db.students.Update(student);
            db.SaveChanges();
        }

        public void UpdateInstructor(Instructor instructor)
        {
            db.instructors.Update(instructor);
            db.SaveChanges();
        }

    }
}
