﻿using Microsoft.AspNetCore.Mvc;
using AttendanceTrackingSystem.Repos;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AttendanceTrackingSystem.Controllers
{
    public class InsTrackController : Controller
    {
        AdTrackRepo Adtrackrepo;

        public InsTrackController(AdTrackRepo _adtrackrepo)
        {
            Adtrackrepo = _adtrackrepo;
        }
        public IActionResult Display(int id=1)
        {
            var model = Adtrackrepo.GetOneTrack(id);
            return View(model);
        }

        public IActionResult GetInstructorsForInsTrack(int id)
        {
            if (id != null)
            {
                var model = Adtrackrepo.GetInstructorsForTrack(id);
                return View(model);
            }
            return View();
        }
        public IActionResult showstudentinInsTrack(int id)
        {
            var track = Adtrackrepo.showstudentinTrack(id);

            if (track == null)
            {
                return NotFound();
            }

            return View(track.Students.ToList());
        }
		public IActionResult Edit(int id)
		{
			var model = Adtrackrepo.GetTrackById(id);
			var program = Adtrackrepo.GetProgramList();
			var ins = Adtrackrepo.GetInstructorList();
			ViewBag.programs = new MultiSelectList(program, "Id", "ProgramName");
			ViewBag.ins = new MultiSelectList(ins, "Id", "Fname");
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(Track track, int id, int supervisorId)
		{
			track.TrackId = id;
			var oldSupervisor = Adtrackrepo.GetSupervisorByTrackId(id);
			if (supervisorId != 0)
			{
				var newSupervisor = Adtrackrepo.GetInstructorById(supervisorId);
				if (newSupervisor != null)
				{
					newSupervisor.Role = Role.Supervisor;
					Adtrackrepo.UpdateInstructor(newSupervisor);
				}
			}
			if (oldSupervisor != null && oldSupervisor.Id != supervisorId)
			{
				oldSupervisor.Role = Role.Instructor;
				Adtrackrepo.UpdateInstructor(oldSupervisor);
			}
			Adtrackrepo.update(track);
			return RedirectToAction("Display", new { id = supervisorId });
		}

	}
}
