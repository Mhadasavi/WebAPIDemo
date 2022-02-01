using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repository.IRepository
{
    public class NationalParkRepository : INationalParkRepository
    {
        public bool CreateNationalPark(NationalPark natioanlPark)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            throw new NotImplementedException();
        }

        public NationalPark GetNationalPark()
        {
            throw new NotImplementedException();
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            throw new NotImplementedException();
        }

        public bool IsNationalParkExist(string name)
        {
            throw new NotImplementedException();
        }

        public bool IsNationalParkExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            throw new NotImplementedException();
        }
    }
}
