using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repos
{
    public interface IAdmin
    {

        public Admin GetAdminByID(int id);


    }

    public class AdminRepo : IAdmin
    {
        readonly ITIDBContext db;

        public AdminRepo(ITIDBContext _db)
        {
            db = _db;
        }


        public Admin GetAdminByID(int id)
        {
            return db.admins.FirstOrDefault(a => a.Id == id);
        }
    }
}
