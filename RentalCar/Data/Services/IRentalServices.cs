using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.Services
{
    public interface IRentalServices
    {
        Task <IEnumerable<Rental>> GetAllAsync();
        Task<Rental> GetByIdAsync(int id);
        Task AddAsync(Rental rental);
        Task<Rental> UpdateAsync(int id, Rental newRental);
        Task DeleteAsync(int id);


    }
}
