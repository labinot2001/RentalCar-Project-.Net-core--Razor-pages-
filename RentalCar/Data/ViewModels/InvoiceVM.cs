using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.ViewModels
{
    public class InvoiceVM
    {
        public Order Order { get; set; }

        public OrderItem OrderItem { get; set; }
        public Car Car { get; set; }


        //public Contact Contacts { get; set; }

    }
}
