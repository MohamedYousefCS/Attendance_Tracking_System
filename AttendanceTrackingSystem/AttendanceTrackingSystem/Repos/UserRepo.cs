using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Repos
{
    public interface IUserRepo
    {
        public int GetUserId(ClaimsPrincipal user);
    }
    public class UserRepo : IUserRepo
    {
        readonly ITIDBContext db;

        public UserRepo(ITIDBContext _db)
        {
            db = _db;
        }

        public int GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
           
            return Convert.ToInt32(userIdClaim.Value);
        }
    }
}


