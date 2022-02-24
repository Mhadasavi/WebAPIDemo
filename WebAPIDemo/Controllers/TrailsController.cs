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
    [Route("api/Trails")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITrailsRepository _trailsRepository;

        public TrailsController(IMapper mapper, ITrailsRepository trailsRepository)
        {
            _mapper = mapper;
            _trailsRepository = trailsRepository;
        }
        /// <summary>
        /// Generates list of Trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(201, Type = typeof(List<TrailsDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetAllTrails()
        {
            var trails = _trailsRepository.GetTrails();
            var trailsList = new List<TrailsDto>();
            foreach (var trail in trails)
            {
                trailsList.Add(_mapper.Map<TrailsDto>(trail));
            }

            return Ok(trailsList);
        }
        /// <summary>
        /// Generate Trails based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetTrails")]
        [ProducesResponseType(201, Type = typeof(List<TrailsDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrails(int id)
        {
            var trails = _trailsRepository.GetTrails(id);
            if (trails != null)
            {
                return Ok(_mapper.Map<TrailsDto>(trails));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrailsDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateTrails([FromBody] TrailsCreateDto trails)
        {
            // var mapper=_mapper.Map<Trails>(trails);
            if (trails == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_trailsRepository.IsTrailsExist(trails.Name))
            {
                ModelState.AddModelError("", "Trails Already Exists");
                return StatusCode(404, ModelState);
            }

            var trailsObj = _mapper.Map<Trails>(trails);
            if (!_trailsRepository.CreateTrails(trailsObj))
            {
                ModelState.AddModelError("", $"Error while saving the record {trails.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrails", new { id = trailsObj.Id }, trailsObj);
        }

        [HttpPatch("{trailsId:int}", Name = "UpdateTrails")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrails(int trailsId, [FromBody] TrailsUpdateDto trails)
        {
            if (trails == null && trailsId != trails.Id)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_trailsRepository.IsTrailsExist(trails.Name))
            {
                ModelState.AddModelError("", "Trails Already Exists");
                return StatusCode(404, ModelState);
            }
            var trailsObj = _mapper.Map<Trails>(trails);
            if (!_trailsRepository.UpdateTrails(trailsObj))
            {
                ModelState.AddModelError("", $"Error while updating the record {trails.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{trailsId:int}", Name = "DeleteTrails")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrails(int trailsId)
        {
            if (!_trailsRepository.IsTrailsExist(trailsId))
            {
                return NotFound();
            }
            var trailsObj = _trailsRepository.GetTrails(trailsId);

            if (!_trailsRepository.DeleteTrails(trailsObj))
            {
                ModelState.AddModelError("", $"Error bs smj jao {trailsObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }

}


