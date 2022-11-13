using Microsoft.EntityFrameworkCore;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.Services
{
    public class RentalServices : IRentalServices
    {
        private readonly AppDbContext _context;
        public RentalServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Rentals.FirstOrDefaultAsync(n => n.Id == id);
            _context.Rentals.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            var result = await _context.Rentals.ToListAsync();
            return result;
        }

        public async Task<Rental> GetByIdAsync(int id)
        {
            var result = await _context.Rentals.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<Rental> UpdateAsync(int id, Rental newRental)
        {
            _context.Update(newRental);
            await _context.SaveChangesAsync();
            return newRental;
        }
    }
}
