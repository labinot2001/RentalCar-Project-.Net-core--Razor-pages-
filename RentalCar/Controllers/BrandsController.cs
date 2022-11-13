using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Data.Services;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace RentalCar.Controllers
{
    [Authorize("Admin")]
    public class BrandsController : Controller
    {
        private readonly IBrandsServices _service;

        public BrandsController(IBrandsServices service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var data = await _service.GetAllAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.FullName.Contains(searchString)
                                       || a.Bio.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.FullName);
                    break;

                default:
                    data = data.OrderBy(a => a.FullName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(data.ToPagedList(pageNumber, pageSize));
        }
        public async Task<IActionResult> Details(int id)
        {
            var brandDetails = await _service.GetByIdAsync(id);
            if (brandDetails == null) return View("Not Found");
            return View(brandDetails);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);

            await _service.AddAsync(brand);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var brandDetails = await _service.GetByIdAsync(id);
            if (brandDetails == null) return View("Not Found");
            return View(brandDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);
            if (id == brand.Id)
            {
                await _service.UpdateAsync(id, brand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);


        }
        public async Task<IActionResult> Delete(int id)
        {
            var brandDetails = await _service.GetByIdAsync(id);
            if (brandDetails == null) return View("Not Found");
            return View(brandDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brandDetails = await _service.GetByIdAsync(id);
            if (brandDetails == null) return View("Not Found");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
