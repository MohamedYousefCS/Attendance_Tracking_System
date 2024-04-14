﻿using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem.Repos
{
    public interface IInstructorRepo
    {
        public List<Instructor> GetInstructorsForTrack(int trackId);
        public List<User> GetInstructorList();
        public List<Track> GetTracks();
        public Track GetTrackById(int id);
        public Track showstudentinTrack(int trackId);
        public void AddTrack(Track track);
        public void updateTrack(Track track);
        public void AddInstructor(Instructor instructor);
        public void updateInstructor(Instructor instructor);
        public void AssignInstructorToTrack(int trackId, int instructorId);
        public User  GetInstructorByID(int id);

    }
    public class InstructorRepo : IInstructorRepo
    {
        readonly ITIDBContext db;

        public InstructorRepo(ITIDBContext _db)
        {
            db = _db;
        }
        public List<Instructor> GetInstructorsForTrack(int trackId)
        {
            var track = db.tracks.FirstOrDefault(a => a.TrackId == trackId);
            var ins = track.Instructors.ToList();
            return ins;
        }

        public List<User> GetInstructorList()
        {
            return db.users.Where(p => p.Role == Role.Instructor).ToList();
        }

        public List<Track> GetTracks()
        {
            return db.tracks.Include(a => a.program).Include(a => a.Instructor).ToList();
        }
        public Track GetTrackById(int id)
        {
            return db.tracks.FirstOrDefault(a => a.TrackId == id);
        }
        public Track showstudentinTrack(int trackId)
        {
            return db.tracks
                     .Include(t => t.Students)
                     .FirstOrDefault(t => t.TrackId == trackId);

        }

        public void AddTrack(Track track)
        {
            db.tracks.Add(track);
            db.SaveChanges();

        }
        public void updateTrack(Track track)
        {
            var model = db.tracks.FirstOrDefault(a => a.TrackId == track.TrackId);
            model.TrackName = track.TrackName;
            model.Status = track.Status;
            model.Capacity = track.Capacity;
            model.programId = track.programId;
            db.SaveChanges();
        }
        public void AddInstructor(Instructor instructor)
        {
            db.instructors.Add(instructor);
            db.SaveChanges();
        }
        public void updateInstructor(Instructor instructor)
        {
            db.instructors.Update(instructor);
            db.SaveChanges();
        }
        public void AssignInstructorToTrack(int trackId, int instructorId)
        {
            var track = db.tracks.FirstOrDefault(a => a.TrackId == trackId);
            var instructor = db.instructors.FirstOrDefault(a => a.Id == instructorId);
            track.Instructor = instructor;
            db.SaveChanges();
        }
        public User GetInstructorByID(int id)
        {
            return db.instructors.FirstOrDefault(a => a.Id == id);
        }
    }

    

}

