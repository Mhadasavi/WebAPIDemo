using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiConsume.Models;
using WebApiConsume.Repository.IRepository;

namespace WebApiConsume.Controllers
{
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
        }
        public IActionResult Index()
        {
            return View(new NationalPark() { });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark nationalPark = new NationalPark();
            if (id == null)
            {
                return View(nationalPark);
            }
            nationalPark = await _nationalParkRepository.GetAsyn(SD.NationalParkPath, id.GetValueOrDefault());
            if (nationalPark == null)
            {
                return NotFound();
            }
            else
            {
                return View(nationalPark);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    obj.Picture = p1;

                }
                else
                {
                    var objFromDb = await _nationalParkRepository.GetAsyn(SD.NationalParkPath, obj.Id);
                    obj.Picture = objFromDb.Picture;
                }
                if (obj.Id == 0)
                {
                    await _nationalParkRepository.CreateAsync(SD.NationalParkPath, obj);
                }
                else
                {
                    await _nationalParkRepository.UpdateAsync(SD.NationalParkPath + obj.Id, obj);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public async Task<IActionResult> GetAllNationalPark()
        {
            return Json(new { data = await _nationalParkRepository.GetAllAsync(SD.NationalParkPath) });
        }
    }
}
