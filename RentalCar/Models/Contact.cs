using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class  Contact 
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Full name")]
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "FullName length must be from 4 to 50 chars ")]
        public string FullName { get; set; }



        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email length must be from 5 to 50 chars ")]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Subject length must be from 4 to 50 chars ")]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Message is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Message length must be from 10 to 300 chars ")]
        public string Message { get; set; }
    }
}
