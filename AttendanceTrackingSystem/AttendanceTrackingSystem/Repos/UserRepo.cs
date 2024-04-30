using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Repos
{
    public interface IUserRepo
    {
        public int GetCurrentUserId(ClaimsPrincipal user);
    }
    public class UserRepo : IUserRepo
    {
        readonly ITIDBContext db;

        public UserRepo(ITIDBContext _db)
        {
            db = _db;
        }

        public int GetCurrentUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "UserId" && int.TryParse(c.Value, out _));

            if (userIdClaim != null)
            {
                return int.Parse(userIdClaim.Value);
            }
            else
            {
                return 0;
            }
        }
    }
}


