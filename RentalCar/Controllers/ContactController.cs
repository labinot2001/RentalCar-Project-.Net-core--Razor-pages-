using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using RentalCar.Data;
using RentalCar.Data.Services;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RentalCar.Controllers
{
    [Authorize("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactServices _service;

        public ContactController(IContactServices service)
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
                                       || a.Email.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.Email);
                    break;

                default:
                    data = data.OrderBy(a => a.FullName);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(data.ToPagedList(pageNumber, pageSize));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var contactDetails = await _service.GetByIdAsync(id);

            if (contactDetails == null) return View("NotFound");
            return View(contactDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactDetails = await _service.GetByIdAsync(id);

            if (contactDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
