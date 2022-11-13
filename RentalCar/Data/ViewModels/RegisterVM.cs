using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name address is required")]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password don't match")]
        public string ConfirmPassword { get; set; }
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
