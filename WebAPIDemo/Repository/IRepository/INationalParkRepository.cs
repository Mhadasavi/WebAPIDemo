using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repository.IRepository
{
    public interface INationalParkRepository
    {
        bool CreateNationalPark(NationalParkDto natioanlPark);
        bool DeleteNationalPark(NationalParkDto nationalPark);
        bool UpdateNationalPark(NationalParkDto nationalPark);
        ICollection<NationalParkDto> GetNationalParks();
        NationalParkDto GetNationalPark(int NationalParkId);
        bool IsNationalParkExist(string name);
        bool IsNationalParkExist(int id);
        bool save();

    }
}
