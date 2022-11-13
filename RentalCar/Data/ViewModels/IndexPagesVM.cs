using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.ViewModels
{
    public class IndexPagesVM
    {
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }

        //public Contact Contacts { get; set; }

    }
}
