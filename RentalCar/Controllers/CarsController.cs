using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Data.Services;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Controllers
{

    public class CarsController : Controller
    {
        private readonly ICarsService _service;

        public CarsController(ICarsService service)
        {
            _service = service;
        }


        [Authorize("Admin")]

        public async Task<IActionResult> Index()
        {
            var allCars = await _service.GetAllAsync(n=>n.Rental);
            return View(allCars);
        }

        [Authorize("Admin")]

        public async Task<IActionResult> Filter(string searchString)
        {
            var allCars = await _service.GetAllAsync(n => n.Rental);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allCars.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allCars);
        }


        //GET: Cars/Details/1
        [Authorize("Admin")]

        public async Task<IActionResult> Details(int id)
        {
            var CarDetails = await _service.GetCarByIdAsync(id);
            return View(CarDetails);
        }
        public async Task<IActionResult> DetailsHome(int id)
        {
            ViewBag.Current = "Offers";

            var CarDetails = await _service.GetCarByIdAsync(id);
            return View(CarDetails);
        }
        //GET: Cars/Create
        [Authorize("Admin")]

        public async Task<IActionResult> Create()
        {
            var carDropdownsData = await _service.GetNewCarDropdownsValues();

            ViewBag.Rentals = new SelectList(carDropdownsData.Rentals, "Id", "Name");
            ViewBag.Brands = new SelectList(carDropdownsData.Brands, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewCarVM car)
        {
            if (!ModelState.IsValid)
            {
                var carDropdownsData = await _service.GetNewCarDropdownsValues();

                ViewBag.Rental = new SelectList(carDropdownsData.Rentals, "Id", "Name");
                ViewBag.Brand = new SelectList(carDropdownsData.Brands, "Id", "FullName");

                return View(car);
            }

            await _service.AddNewCarAsync(car);
            return RedirectToAction(nameof(Index));
        }


        //GET: Cars/Edit/1
        [Authorize("Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            var CarDetails = await _service.GetCarByIdAsync(id);
            if (CarDetails == null) return View("NotFound");

            var response = new NewCarVM()
            {
                Id = CarDetails.Id,
                Name = CarDetails.Name,
                Description = CarDetails.Description,
                Price = CarDetails.Price,
                StartDate = CarDetails.StartDate,
                EndDate = CarDetails.EndDate,
                ImageURL = CarDetails.ImageURL,
                CarCategory = CarDetails.CarCategory,
                RentalId = CarDetails.RentalId,
                BrandId = CarDetails.BrandId

            };

            var carDropdownsData = await _service.GetNewCarDropdownsValues();
            ViewBag.Rental = new SelectList(carDropdownsData.Rentals, "Id", "Name");
            ViewBag.Brand = new SelectList(carDropdownsData.Brands, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewCarVM Car)
        {
            if (id != Car.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var carDropdownsData = await _service.GetNewCarDropdownsValues();

                ViewBag.Rental = new SelectList(carDropdownsData.Rentals, "Id", "Name");
                ViewBag.Brand = new SelectList(carDropdownsData.Brands, "Id", "FullName");

                return View(Car);
            }

            await _service.UpdateCarAsync(Car);
            return RedirectToAction(nameof(Index));

        }
        [Authorize("Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var carDetails = await _service.GetByIdAsync(id);
            if (carDetails == null) return View("Not Found");
            return View(carDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carDetails = await _service.GetByIdAsync(id);
            if (carDetails == null) return View("Not Found");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }


    }
}
