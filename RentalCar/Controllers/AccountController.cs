using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Data.Static;
using RentalCar.Data.ViewModels;
using RentalCar.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using X.PagedList;
using System.Collections.Generic;
using RentalCar.Data.Cart;
using Microsoft.AspNetCore.Authorization;
using RentalCar.Data.Services;
using System.Security.Claims;

namespace RentalCar.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ShoppingCart _shoppingCart;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IOrdersService _ordersService;

        public AccountController(RoleManager<IdentityRole> roleManager, ShoppingCart shoppingCart ,UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, AppDbContext context, IOrdersService ordersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }
        //public async Task<IActionResult> Users()
        //{
        //    var users = await _context.Users.ToListAsync();

        //    return View(users);
        //}
        [HttpGet]
        [Authorize("Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName,
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Account");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        [Authorize("Admin")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        [Authorize("Admin")]

        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in _userManager.Users.ToList())
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }
        [HttpGet]
        [Authorize("Admin")]

        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");

            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });

                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });

        }

        [Authorize("Admin")]

        public ActionResult Admins(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var admins = from user in _context.Users
                         join userRole in _context.UserRoles
                         on user.Id equals userRole.UserId
                         join role in _context.Roles
                         on userRole.RoleId equals role.Id
                         where role.Name == "Admin"
                         select user;
            //.ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                admins = admins.Where(a => a.UserName.Contains(searchString)
                                       || a.Email.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    admins = admins.OrderByDescending(a => a.Email);
                    break;

                default:
                    admins = admins.OrderBy(a => a.UserName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(admins.ToPagedList(pageNumber, pageSize));
        }
        //var usersWithRoles = (from user in _context.Users
        //                      select new
        //                      {
        //                          UserId = user.Id,
        //                          Username = user.UserName,
        //                          Email = user.Email,
        //                          RoleNames = (from userRole in _context.UserRoles
        //                                       join role in _context.UserRoles on userRole.UserId
        //                                       equals role.UserId
        //                                       select role.UserId)
        //                      }).Select(p => new Users_in_Role_ViewModel()

        //                      {
        //                          UserId = p.UserId,
        //                          Username = p.Username,
        //                          Email = p.Email,
        //                          Role = string.Join(",", p.RoleNames)
        //                      });
        //return View(admins);
        [Authorize("Admin")]

        public ActionResult Users(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var users = from user in _context.Users
                        join userRole in _context.UserRoles
                        on user.Id equals userRole.UserId
                        join role in _context.Roles
                        on userRole.RoleId equals role.Id
                        where role.Name == "User"
                        select user;
            //.ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(a => a.UserName.Contains(searchString)
                                       || a.Email.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(a => a.Email);
                    break;
                case "fullname_desc":
                    users = users.OrderByDescending(a => a.FullName);
                    break;

                default:
                    users = users.OrderBy(a => a.UserName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentails. Please try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentails. Please try again!";
            return View(loginVM);
        }
        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email is already in use!";
                return View(registerVM);
            }
            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                DrivingLicenseID = registerVM.DrivingLicenseID,
                City = registerVM.City,
                Address = registerVM.Address,
                Payment = registerVM.Payment
            };
            var newUserResponese = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponese.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");

        }
        [Authorize("Admin")]
        public IActionResult RegisterAdmin() => View(new RegisterVM());

        [HttpPost]

        public async Task<IActionResult> RegisterAdmin(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email is already in use!";
                return View(registerVM);
            }
            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                DrivingLicenseID = registerVM.DrivingLicenseID,
                City = registerVM.City,
                Address = registerVM.Address,
                Payment = registerVM.Payment
            };
            var newUserResponese = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponese.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);

            return View("RegisterCompleted");

        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return View("NotFound");

            var model = new EditUserVM()
            {
                FullName = user.FullName,
                EmailAddress = user.Email,
                DrivingLicenseID = user.DrivingLicenseID,
                City = user.City,
                Address = user.Address,
                Payment = user.Payment
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM model)
        {
            
            var user = await _userManager.FindByIdAsync(model.Id);
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            if (user == null){
                return View("NotFound");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
               
                user.FullName = model.FullName;
                user.Email = model.EmailAddress;
                user.DrivingLicenseID = model.DrivingLicenseID;
                user.City = model.City;
                user.Address = model.Address;
                user.Payment = model.Payment;
                
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
                    await _shoppingCart.ClearShoppingCartAsync();
                    return RedirectToAction("Invoice", "Orders");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("Invoice", "Orders");

            }



        }

        //public async Task<IActionResult> EditUser(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user == null) return View("NotFound");
        //    var response = new RegisterVM()
        //    {
        //        FullName = user.FullName,
        //        EmailAddress = user.Email,
        //        DrivingLicenseID = user.DrivingLicenseID,
        //        City = user.City,
        //        Address = user.Address,
        //        Payment = user.Payment
        //    };

        //    return View(response);
        //}







        //[HttpPost]
        //public async Task<IActionResult> EditUser(string email, RegisterVM response)
        //{

        //    if (email != response.EmailAddress)
        //    {
        //        ViewBag.ErrorMessage = $"User with email = {response.EmailAddress} cannot be found";
        //        return View("NotFound");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return View(response);
        //    }
        //    //}else
        //    //{


        //    var result = await _userManager.UpdateAsync();

      
        //    //};

        //}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
    }
}
