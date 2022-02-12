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

        [HttpGet("{id:int}", Name = "GetNationalPark")]
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
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalPark)
        {
            // var mapper=_mapper.Map<NationalPark>(nationalPark);
            if (nationalPark == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_nationalParkRepository.IsNationalParkExist(nationalPark.Name))
            {
                ModelState.AddModelError("", "National Park Already Exists");
                return StatusCode(404, ModelState);
            }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalPark);
            if (!_nationalParkRepository.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Error while saving the record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNationalPark", new { id = nationalParkObj.Id }, nationalParkObj);
        }
        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalPark)
        {
            if (nationalPark == null && nationalParkId != nationalPark.Id)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_nationalParkRepository.IsNationalParkExist(nationalPark.Name))
            {
                ModelState.AddModelError("", "National Park Already Exists");
                return StatusCode(404, ModelState);
            }
            var nationalParkObj = _mapper.Map<NationalPark>(nationalPark);
            if (!_nationalParkRepository.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Error while updating the record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalParkRepository.IsNationalParkExist(nationalParkId))
            {
                return NotFound();
            }
            var nationalParkObj = _nationalParkRepository.GetNationalPark(nationalParkId);

            if (!_nationalParkRepository.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Error bs smj jao {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }

}


