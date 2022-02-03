using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Dtos;
using WebAPIDemo.Repository.IRepository;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParkController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INationalParkRepository _nationalParkRepository;

        public NationalParkController(IMapper mapper, INationalParkRepository nationalParkRepository)
        {
            _mapper = mapper;
            _nationalParkRepository = nationalParkRepository;
        }

        [HttpGet]
        public IActionResult GetAllNationalParks()
        {
            var nationalParks = _nationalParkRepository.GetNationalParks();
            var nationalParkList = new List<NationalParkDto>();
            foreach (var park in nationalParks)
            {
                nationalParkList.Add(_mapper.Map<NationalParkDto>(park));
            }

            return Ok(nationalParkList);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetNationalPark(int id)
        {
            var nationalPark = _nationalParkRepository.GetNationalPark(id);
            if (nationalPark != null)
            {
                return Ok(_mapper.Map<NationalParkDto>(nationalPark));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateNationalPark(NationalParkDto nationalPark)
        {
            // var mapper=_mapper.Map<NationalPark>(nationalPark);
            _nationalParkRepository.CreateNationalPark(_mapper.Map<NationalPark>(nationalPark));
            return Ok();
        }
    }
}
