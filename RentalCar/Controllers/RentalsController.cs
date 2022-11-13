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
    public class RentalsController : Controller
    {
        private readonly IRentalServices _service;

        public RentalsController(IRentalServices service)
        {
            _service = service;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var data = await _service.GetAllAsync();
        //    return View(data); 
        //}
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
                data = data.Where(a => a.Name.Contains(searchString)
                                       || a.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.Name);
                    break;

                default:
                    data = data.OrderBy(a => a.Description);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(data.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo, Name, Address, Mobile, Description")] Rental rental)
        {
            if (!ModelState.IsValid) return View(rental);
            await _service.AddAsync(rental);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id) 
        {
            var rentalDetails = await _service.GetByIdAsync(id);

            if (rentalDetails == null) return View("NotFound");
            return View(rentalDetails);
        }
        //Get: cinemas Edit
        public async Task<IActionResult> Edit(int id)
        {
            var rentalDetails = await _service.GetByIdAsync(id);
            if (rentalDetails == null) return View("NotFound");
            return View(rentalDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id ,[Bind("Id,Logo, Name, Address, Mobile, Description")] Rental rental)
        {
            if (ModelState.IsValid) return View(rental);
            await _service.UpdateAsync(id, rental);
            return RedirectToAction(nameof(Index));
        }
        //Get: cinemas Delete
        public async Task<IActionResult> Delete(int id)
        {
            var rentalDetails = await _service.GetByIdAsync(id);
            if (rentalDetails == null) return View("NotFound");
            return View(rentalDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var rentalDetails = await _service.GetByIdAsync(id);
            if (rentalDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
