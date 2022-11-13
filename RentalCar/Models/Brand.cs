using RentalCar.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class  Brand:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Brand")]
        [Required(ErrorMessage ="Profile Picture is Required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name =" Name")]
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50,MinimumLength = 3,ErrorMessage ="Name Must be betwen 3 and 50 chars")]
        public string FullName { get; set; }
        [Display(Name="Description")]
        [Required(ErrorMessage = "Description is Required")]
        public string Bio { get; set; }

        //Relationships
        public List<Car> Cars { get; set; }
    }
}
