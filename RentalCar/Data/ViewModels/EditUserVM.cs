using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.ViewModels
{
    public class EditUserVM
    {
        public string Id { get; set; }
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name address is required")]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        //[Display(Name = "User")]
        //[Required(ErrorMessage = "Email address is required")]
        //public string UserName { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Display(Name = "Confirm password")]

        //[Required(ErrorMessage = "Confirm password is required")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Password don't match")]
        //public string ConfirmPassword { get; set; }
        [Display(Name = "Driving license(p.s:123456789)")]
        [Required(ErrorMessage = "DrivingLicenseID is required")]
        [Range(100000000, 999999999)]
        public int DrivingLicenseID { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Message length must be from 3 to 50 chars ")]

        public string City { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Address length must be from 3 to 50 chars ")]
        public string Address { get; set; }
        [Display(Name = "Select a payment method")]
        [Required(ErrorMessage = "Payment select is required")]
        public Payment Payment { get; set; }
    }
}
