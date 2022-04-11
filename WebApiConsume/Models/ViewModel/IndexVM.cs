using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiConsume.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<NationalPark> NationalParksList { get; set; }
        public IEnumerable<Trails> TrailsList { get; set; }
    }
}
