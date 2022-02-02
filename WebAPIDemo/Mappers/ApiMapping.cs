using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Dtos;

namespace WebAPIDemo.Mappers
{
    public class ApiMapping:Profile
    {
        public ApiMapping()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
        }
    }
}
