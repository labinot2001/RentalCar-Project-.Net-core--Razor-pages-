using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Data.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}