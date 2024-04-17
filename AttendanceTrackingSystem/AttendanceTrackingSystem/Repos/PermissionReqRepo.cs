using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repos
{
    public class PermissionReqRepo
    {
        readonly ITIDBContext db;

        public PermissionReqRepo(ITIDBContext _db) {
          db = _db;
        }

        public List<PermissionRequest> GetPermissions()
        {
           return db.permissionRequests.ToList();
        }
        public PermissionRequest GetPermissionById(int id)
        {
            return  db.permissionRequests.FirstOrDefault(a=>a.RequestID==id);
        }
        public void  savecahaanges ()
        {
             db.SaveChanges();
        }



    }
}
