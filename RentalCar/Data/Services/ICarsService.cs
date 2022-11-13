using RentalCar.Data.Base;
using RentalCar.Data.ViewModels;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.Services
{
    public interface ICarsService : IEntityBaseRepository<Car>
    {



        Task<Car> GetCarByIdAsync(int id);
        Task AddNewCarAsync(NewCarVM data);
        Task UpdateCarAsync(NewCarVM data);
       
        Task<NewCarDropdownsVM> GetNewCarDropdownsValues();

    }


}
