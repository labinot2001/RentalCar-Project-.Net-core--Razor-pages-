using RentalCar.Data;
using RentalCar.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class Car : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CarCategory CarCategory { get; set; }


        //Relationships

        //Rental
        public int RentalId { get; set; }
        [ForeignKey("RentalId")]
        public Rental Rental { get; set; }

        //Brands
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }




    }
}
