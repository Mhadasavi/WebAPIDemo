using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Data;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repository.IRepository
{
    public class TrailsRepository : ITrailsRepository
    {
        private readonly ApplicationDbContext _db;
        public TrailsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateTrails(Trails trail)
        {
            _db.Trails.Add(trail);
            return save();
        }

        public bool DeleteTrails(Trails trail)
        {
            _db.Trails.Remove(trail);
            return save();
        }

        public Trails GetTrails(int TrailsId)
        {
            return _db.Trails.Include(c => c.NationalPark).FirstOrDefault(i => i.Id == TrailsId);

        }

        public ICollection<Trails> GetTrails()
        {
            return _db.Trails.Include(c => c.NationalPark).OrderBy(i => i.Name).ToList();
        }

        public bool IsTrailsExist(string name)
        {
            bool trail = _db.Trails.Any(o => o.Name.ToLower().Trim() == name.ToLower().Trim());
            return trail;
        }

        public bool IsTrailsExist(int id)
        {
            bool trail = _db.Trails.Any(i => i.Id == id);
            return trail;
        }

        public bool save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrails(Trails trail)
        {
            _db.Trails.Update(trail);
            return save();
        }

        ICollection<Trails> ITrailsRepository.GetTrailsInNationalPark(int id)
        {
            return _db.Trails.Include(c=>c.NationalPark).Where(c=>c.NationalParkId==id).ToList();
        }
    }
}
