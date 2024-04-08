using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AttendanceTrackingSystem.Controllers
{
    public class TrackController : Controller
    {
        AdTrackRepo Adtrackrepo;

        public TrackController(AdTrackRepo _adtrackrepo)
        {
            Adtrackrepo= _adtrackrepo;
        }
        public IActionResult Display()
        {
            var model = Adtrackrepo.GetTracks();
            return View(model);
        }

        public IActionResult GetInstructorsForTrack(int id)
        {
            if (id != null)
            {
                var model = Adtrackrepo.GetInstructorsForTrack(id);
                return View(model);
            }
            return View();
        }

        public IActionResult showstudentinTrack(int id)
        {
            var track = Adtrackrepo.showstudentinTrack(id);

            if (track == null)
            {
                return NotFound(); 
            }

            return View(track.Students.ToList());
        }
        public IActionResult Create()
        {
           var program=Adtrackrepo.GetProgramList();
            var ins = Adtrackrepo.GetInstructorList();
            ViewBag.programs= new MultiSelectList(program, "Id", "ProgramName");
            ViewBag.ins=new MultiSelectList(ins,"Id" ,"Fname");
            return View(new Track());
        }
        [HttpPost]
        public IActionResult Create(Track track) {
            Adtrackrepo.Add(track);
            return RedirectToAction("Display");

        }
        public IActionResult Edit(int id)
        {
            var model = Adtrackrepo.GetTrackById(id);
             var program=Adtrackrepo.GetProgramList();
            var ins = Adtrackrepo.GetInstructorList();
            ViewBag.programs= new MultiSelectList(program, "Id", "ProgramName");
            ViewBag.ins=new MultiSelectList(ins,"Id" ,"Fname");
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Track track, int id)
        {
            track.TrackId = id;
            Adtrackrepo.update(track);
            return RedirectToAction("Display");
        }
        public IActionResult Delete(int? id)
        {
            Track track = Adtrackrepo.showstudentinTrack(id.Value);
           
                if (track.Students.Count != 0)
                {

                    TempData["AlertMessage"] = "Cannot delete track";
                }
            
            else
            {
                Adtrackrepo.Delete(track);
            }

            return RedirectToAction("Display");
        }



    }
}
