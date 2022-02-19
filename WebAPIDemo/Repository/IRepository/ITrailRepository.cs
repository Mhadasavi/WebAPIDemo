using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repository.IRepository
{
    public interface ITrailsRepository
    {
        bool CreateTrails(Trails trail);
        bool DeleteTrails(Trails trail);
        bool UpdateTrails(Trails trail);
        ICollection<Trails> GetTrails();
        ICollection<Trails> GetTrailsInNationalPark(int id);
        Trails GetTrails(int TrailsId);
        bool IsTrailsExist(string name);
        bool IsTrailsExist(int id);
        bool save();

    }
}
