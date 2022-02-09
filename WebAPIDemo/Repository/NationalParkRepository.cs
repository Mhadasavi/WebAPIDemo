using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Data;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repository.IRepository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;
        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return save();
        }

        public NationalPark GetNationalPark(int NationalParkId)
        {
            return _db.NationalParks.FirstOrDefault(i => i.Id == NationalParkId);

        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(i => i.Name).ToList();
        }

        public bool IsNationalParkExist(string name)
        {
            bool nationalPark = _db.NationalParks.Any(o => o.Name.ToLower().Trim() == name.ToLower().Trim());
            return nationalPark;
        }

        public bool IsNationalParkExist(int id)
        {
            bool nationalPark = _db.NationalParks.Any(i => i.Id == id);
            return nationalPark;
        }

        public bool save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(UpdateNationalPark nationalPark)
        {
           // _db.NationalParks.Update(nationalPark);
            return save();
        }
    }
}
