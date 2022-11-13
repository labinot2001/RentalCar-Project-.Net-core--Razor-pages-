using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
