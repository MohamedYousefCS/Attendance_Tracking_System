﻿using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace AttendanceTrackingSystem.Repos
{
    public class AdTrackRepo
    {
        readonly ITIDBContext db;

        public AdTrackRepo(ITIDBContext _db)
        {
            db = _db;
        }
        public List<program>   GetProgramList()
        {
            return db.programs.ToList();
        }
        public List<Instructor> GetInstructorsForTrack(int trackId)
        {
           
             var track=db.tracks.FirstOrDefault(a=>a.TrackId==trackId);
            var ins=track.Instructors.ToList();
            return ins;
        }

        public List<User> GetInstructorList()
        {
            return db.users.Where(p=>p.Role==Role.Instructor).ToList();
        }
       
        public List<Track> GetTracks()
        {
            return db.tracks.Include(a=>a.program).Include(a=>a.Instructor).ToList();
        }
        public  Track GetTrackById(int id) {
            return db.tracks.FirstOrDefault(a => a.TrackId == id);
        }
        public Track showstudentinTrack(int trackId)
        {
            return db.tracks
                     .Include(t => t.Students)
                     .FirstOrDefault(t => t.TrackId == trackId);
        
        }

        public void Add(Track track)
        {
            db.tracks.Add(track);
            db.SaveChanges();

        }
        public void update(Track track)
        {
            var model = db.tracks.FirstOrDefault(a => a.TrackId == track.TrackId);
            model.TrackName=track.TrackName;
            model.Status = track.Status;
            model.Capacity= track.Capacity;
            model.programId=track.programId;
            model.supervisorId=track.supervisorId;
            db.SaveChanges();
        }
        public void Delete(Track track)
        {
                    db.tracks.Remove(track);
                    db.SaveChanges(); 
        }
      

    }
}