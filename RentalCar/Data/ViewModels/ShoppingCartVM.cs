using Microsoft.AspNetCore.Identity;
using RentalCar.Data.Cart;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.ViewModels
{
    public class ShoppingCartVM
    {
        public ApplicationUser ApplicationUser{ get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }

    }
}
