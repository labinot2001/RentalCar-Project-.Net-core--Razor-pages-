using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.ViewModels
{
    public class DetailsCarUserVM
    {
        public Car Cars { get; set; }
        public ApplicationUser Users { get; set; }

        //public Contact Contacts { get; set; }

    }
}
