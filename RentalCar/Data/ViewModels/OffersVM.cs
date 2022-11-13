using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.ViewModels
{
    public class OffersVM
    {
        public IEnumerable<Car> Cars { get; set; }

        public IEnumerable<Brand> Brands { get; set; }

    }
}
