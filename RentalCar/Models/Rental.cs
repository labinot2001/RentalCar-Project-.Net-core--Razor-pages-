using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Logo is required")]
        [StringLength(2500, MinimumLength = 5, ErrorMessage = "Logo must be 5 to 2500 chars")]
        public string Logo { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be 3 to 50 chars")]
        public string Name { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Address length must be 5 to 50 chars")]
        public string Address { get; set; }
        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Mobile Number is required")]
        [Range(3800000, 49000000, ErrorMessage = "Mobile Number must be 044123123 ")]
        public int Mobile { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Desciption is required")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Description length must be 10 to 100 chars")]
        public string Description { get; set; }



        //Realationship --L.K   (Cars --> Rental)
        public List<Car> Cars { get; set; }



    }
}
