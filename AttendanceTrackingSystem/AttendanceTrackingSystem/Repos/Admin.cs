using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repos
{
    public interface IAdmin
    {

        public User GetAdminByID(int id);


    }

    public class Admin : IAdmin
    {
        readonly ITIDBContext db;

        public Admin(ITIDBContext _db)
        {
            db = _db;
        }



        public User GetAdminByID(int id)
        {
            return db.admins.FirstOrDefault(a => a.Id == id);
        }
    }
}
