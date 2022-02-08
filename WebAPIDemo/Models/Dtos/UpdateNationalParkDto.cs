using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDemo.Models.Dtos
{
    public class UpdateNationalParkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
       
    }
}
