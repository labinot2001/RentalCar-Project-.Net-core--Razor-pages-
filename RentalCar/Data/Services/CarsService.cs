using Microsoft.EntityFrameworkCore;
using RentalCar.Data.Base;
using RentalCar.Data.ViewModels;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.Services
{
    public class CarsService : EntityBaseRepository<Car>, ICarsService
    {
        private readonly AppDbContext _context;
        public CarsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewCarAsync(NewCarVM data)
        {

            var newCar = new Car()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                RentalId = data.RentalId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                CarCategory = data.CarCategory,
                BrandId = data.BrandId
            };
            await _context.Cars.AddAsync(newCar);
            await _context.SaveChangesAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {

            var CarDetails = _context.Cars
              .Include(r => r.Rental)
              .Include(p => p.Brand)
              .FirstOrDefaultAsync(n => n.Id == id);

            return await CarDetails;
        }


        public async Task<NewCarDropdownsVM> GetNewCarDropdownsValues()
        {
            var response = new NewCarDropdownsVM()
            {
                Rentals = await _context.Rentals.OrderBy(n => n.Name).ToListAsync(),
                 Brands = await _context.Brands.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }


        public async Task UpdateCarAsync(NewCarVM data)
        {
            var dbCar = await _context.Cars.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbCar != null)
            {
                dbCar.Name = data.Name;
                dbCar.Description = data.Description;
                dbCar.Price = data.Price;
                dbCar.ImageURL = data.ImageURL;
                dbCar.RentalId = data.RentalId;
                dbCar.StartDate = data.StartDate;
                dbCar.EndDate = data.EndDate;
                dbCar.CarCategory = data.CarCategory;
                dbCar.BrandId = data.BrandId;
                await _context.SaveChangesAsync();


            }
        }

        // in this line ...
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Cars.FirstOrDefaultAsync(n => n.Id == id);
            _context.Cars.Remove(result);
            await _context.SaveChangesAsync();
        }
        


    }
}
