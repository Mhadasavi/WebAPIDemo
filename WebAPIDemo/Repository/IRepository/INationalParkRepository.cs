using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repository.IRepository
{
    public interface INationalParkRepository
    {
        bool CreateNationalPark(NationalPark natioanlPark);
        bool DeleteNationalPark(NationalPark nationalPark);
        bool UpdateNationalPark(NationalPark nationalPark);
        ICollection<NationalPark> GetNationalParks();
        NationalPark GetNationalPark();
        bool IsNationalParkExist(string name);
        bool IsNationalParkExist(int id);
        bool save();

    }
}
