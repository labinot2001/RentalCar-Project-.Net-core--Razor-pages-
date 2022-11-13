using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.Services
{
    public interface IBrandsServices
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand> GetByIdAsync(int id);
        Task AddAsync(Brand client);
        Task<Brand> UpdateAsync(int id, Brand newBrand);
        Task DeleteAsync(int id);

    }
}
