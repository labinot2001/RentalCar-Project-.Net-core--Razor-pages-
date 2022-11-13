using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.ViewModels
{
    public class NewCarDropdownsVM
    {

        public NewCarDropdownsVM()
        {
            Brands = new List<Brand>();
            Rentals = new List<Rental>();
        }

        public List<Rental> Rentals { get; set; }
        public List<Brand> Brands { get; set; }



    }
}
