using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Controllers
{
    public class PermissionReqController : Controller
    {

        PermissionReqRepo PermissionreqRepo;

            public PermissionReqController(PermissionReqRepo _permissionreqRepo)
            {
            PermissionreqRepo = _permissionreqRepo;
             }
        public IActionResult Display()
        {
            var model = PermissionreqRepo.GetPermissions().Where(pr =>pr.IsAccepted==IsAccepted.pending).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult AcceptPermission(int id)
        {
            var permissionRequest = PermissionreqRepo.GetPermissionById(id);
            if (permissionRequest == null)
            {
                return NotFound();
            }

            permissionRequest.IsAccepted = IsAccepted.Accepted;
            PermissionreqRepo.savecahaanges();

            return RedirectToAction("Display");
        }
        public IActionResult RejectPermission(int id)
        {
            var permissionRequest = PermissionreqRepo.GetPermissionById(id);
            if (permissionRequest == null)
            {
                return NotFound();
            }

            permissionRequest.IsAccepted = IsAccepted.Rejected;
            PermissionreqRepo.savecahaanges();

            return RedirectToAction("Display");
        }

    }
}
