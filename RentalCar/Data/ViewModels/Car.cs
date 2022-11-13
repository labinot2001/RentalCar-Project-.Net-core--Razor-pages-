using RentalCar.Data;
using RentalCar.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class NewCarVM
    {

        public int Id { get; set; }
        [Display(Name = "Car name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "Car description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Display(Name = "Car poster URL")]
        [Required(ErrorMessage = "Car poster URL is required")]
        public string ImageURL { get; set; }
        [Display(Name = "Car start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Car end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Car category is required")]

        public CarCategory CarCategory { get; set; }
        
        [Display(Name = "Select a Rental")]
        [Required(ErrorMessage = "Car rental is required")]
        public int RentalId { get; set; }

        [Display(Name = "Select a brand")]
        [Required(ErrorMessage = "Car brand is required")]
        public int BrandId { get; set; }

    }
}
