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
    [ApiExplorerSettings(GroupName = "NationalPark")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParkController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INationalParkRepository _nationalParkRepository;

        public NationalParkController(IMapper mapper, INationalParkRepository nationalParkRepository)
        {
            _mapper = mapper;
            _nationalParkRepository = nationalParkRepository;
        }
        /// <summary>
        /// Generates list of national parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(201, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Generate National Park based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetNationalPark")]
        [ProducesResponseType(201, Type = typeof(List<NationalParkDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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


