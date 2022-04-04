using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiConsume.Models;
using WebApiConsume.Models.ViewModel;
using WebApiConsume.Repository.IRepository;

namespace WebApiConsume.Controllers
{
    public class TrailsController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly ITrailsRepository _trailsRepository;
        public TrailsController(ITrailsRepository trailsRepository, INationalParkRepository nationalParkRepository)
        {
            _trailsRepository = trailsRepository;
            _nationalParkRepository = nationalParkRepository;
        }
        public IActionResult Index()
        {
            return View(new Trails() { });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> nationalParks = await _nationalParkRepository.GetAllAsync(SD.NationalParkPath);

            TrailsVM obj = new TrailsVM()
            {
                NationalParks = nationalParks.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Trail = new Trails()
            };

            if (id == null)
            {
                return View(obj);
            }
            obj.Trail = await _trailsRepository.GetAsyn(SD.TrailsPath, id.GetValueOrDefault());
            if (obj.Trail == null)
            {
                return NotFound();
            }
            else
            {
                return View(obj);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailsVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Trail.Id == 0)
                {
                    await _trailsRepository.CreateAsync(SD.TrailsPath, obj.Trail);
                }
                else
                {
                    await _trailsRepository.UpdateAsync(SD.TrailsPath + obj.Trail.Id, obj.Trail);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public async Task<IActionResult> GetAllTrails()
        {
            return Json(new { data = await _trailsRepository.GetAllAsync(SD.TrailsPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _trailsRepository.DeleteAsync(SD.TrailsPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete successful" });
            }
            return Json(new { success = false, message = "Delete Unsuccessful" });
        }
    }
}
