using Microsoft.AspNetCore.Identity;
using RentalCar.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name address is required")]
        public string FullName { get; set; }

      
        [Display(Name = "Driving license(p.s:123456789)")]
        //[Range(100000000,999999999)]
        public int DrivingLicenseID { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Select a payment method")]
        public Payment Payment { get; set; }

    }
}
