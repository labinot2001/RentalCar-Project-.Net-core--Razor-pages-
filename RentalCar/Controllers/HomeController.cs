using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentalCar.Data;
using RentalCar.Data.Services;
using RentalCar.Data.ViewModels;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarsService _service;
        private readonly IRentalServices _rentalServices;
        private readonly IBrandsServices _brandsServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IContactServices _contactServices;


        private readonly AppDbContext _context;




        public HomeController(ILogger<HomeController> logger,IBrandsServices brandsServices ,ICarsService service,IRentalServices rentalServices, IContactServices contactServices, AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _brandsServices = brandsServices;
            _logger = logger;
            _service = service;
            _rentalServices = rentalServices;
            _context = context;
            _contactServices = contactServices;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Current = "Index";

            var AllCars = await _service.GetAllAsync(n=>n.Rental,n=>n.Brand);
            var users = await (from user in _context.Users
                               join userRole in _context.UserRoles
                               on user.Id equals userRole.UserId
                               join role in _context.Roles
                               on userRole.RoleId equals role.Id
                               where role.Name == "User"
                               select user).ToListAsync();
            return View(new IndexPagesVM { Users = users, Cars = AllCars });
        }
        public async Task<IActionResult> RentKosova()
        {
            ViewBag.Current = "Fleet";
            var category = await (from car in _context.Cars
                                  where car.Rental.Name == "Rent Kosova"
                                  select car).Include(m=>m.Rental).ToListAsync();




            var brands = await _brandsServices.GetAllAsync();

            return View(new OffersVM { Cars = category, Brands = brands });
        }
        public async Task<IActionResult> BesianaRent()
        {
            ViewBag.Current = "Fleet";
            var category = await (from car in _context.Cars
                                  where car.Rental.Name == "Besiana Rent"
                                  select car).Include(m => m.Rental).ToListAsync();



            var brands = await _brandsServices.GetAllAsync();

            return View(new OffersVM { Cars = category, Brands = brands });
        }
        public async Task<IActionResult> IsmajliRent()
        {
            ViewBag.Current = "Fleet";
            var category = await (from car in _context.Cars
                                  where car.Rental.Name == "Ismajli Rent"
                                  select car).Include(m => m.Rental).ToListAsync();



            var brands = await _brandsServices.GetAllAsync();

            return View(new OffersVM { Cars = category, Brands = brands });
        }
        public async Task<IActionResult> KrasniqiRent()
        {
            ViewBag.Current = "Fleet";
            var category = await (from car in _context.Cars
                                  where car.Rental.Name == "Krasniqi Rent"
                                  select car).Include(m => m.Rental).ToListAsync();



            var brands = await _brandsServices.GetAllAsync();

            return View(new OffersVM { Cars = category, Brands = brands });
        }
        public async Task<IActionResult> AutoRentBeka()
        {
            ViewBag.Current = "Fleet";
            var category = await (from car in _context.Cars
                                  where car.Rental.Name == "Auto Rent Beka"
                                  select car).Include(m => m.Rental).ToListAsync();



            var brands = await _brandsServices.GetAllAsync();

            return View(new OffersVM { Cars = category, Brands = brands });
        }
        public async Task<IActionResult> LlapiRent()
        {
            ViewBag.Current = "Fleet";
            var category = await (from car in _context.Cars
                                  where car.Rental.Name == "Llapi Rent"
                                  select car).Include(m => m.Rental).ToListAsync();



            var brands = await _brandsServices.GetAllAsync();

            return View(new OffersVM { Cars = category, Brands = brands });
        }


        public IActionResult AboutUs()
        {
            ViewBag.Current = "AboutUs";

            return View();
        }
        public IActionResult Blog()
        {
            ViewBag.Current = "Blog";

            return View();
        }
        public IActionResult BlogDetails()
        {
            ViewBag.Current = "BlogDetails";
            return View();
        }
        public IActionResult Contact()
        {
            ViewBag.Current = "Contact";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact([Bind("FullName, Email, Subject, Message")] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            await _contactServices.AddAsync(contact);
            ViewBag.Message = "Send Successfully";
            int id = contact.Id;
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Current = "Contact";

            var contactDetails = await _contactServices.GetByIdAsync(id);

            if (contactDetails == null) return View("NotFound");
            return View(contactDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contactDetails = await _contactServices.GetByIdAsync(id);

            if (contactDetails == null) return View("NotFound");
            return View(contactDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, Email, Subject, Message")] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            await _contactServices.UpdateAsync(id, contact);
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<IActionResult> Fleet()
        {
            ViewBag.Current = "Fleet";
            var rentals = await _rentalServices.GetAllAsync( /*n=> n.Cinema*/);

            return View(rentals);
        }
        public async Task<IActionResult> Offers()
        {
            ViewBag.Current = "Offers";
            var cars =await _service.GetAllAsync(n=>n.Rental);
            var brands = await _brandsServices.GetAllAsync();

            return View(new OffersVM{Cars = cars, Brands = brands });
        }
        public IActionResult Team()
        {
            ViewBag.Current = "Team";
            return View();
        }
        public IActionResult Terms()
        {
            ViewBag.Current = "Terms";
            return View();
        }
        public async Task<IActionResult> Testimonials()
        {
            ViewBag.Current = "Testimonails";
            var users = await(from user in _context.Users
                              join userRole in _context.UserRoles
                              on user.Id equals userRole.UserId
                              join role in _context.Roles
                              on userRole.RoleId equals role.Id
                              where role.Name == "User"
                              select user).ToListAsync();
            return View( users);
        }
        /*------------------------*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
