using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCar.Data.Services
{
    public interface IContactServices
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task AddAsync(Contact contact);
        Task<Contact> UpdateAsync(int id, Contact newContact);
        Task DeleteAsync(int id);

    }
}
