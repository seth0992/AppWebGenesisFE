using AppWebGenesisFE.Database.Data;
using AppWebGenesisFE.Models.Entities.Customer;
using AppWebGenesisFE.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.BL.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<CustomerModel>> GetCustomers();

        Task<CustomerModel> CreateCustomer(CustomerModel customer);

        Task<CustomerModel> GetCustomer(long idCustomer);
        Task<bool> CustomerModelExist(long idCustomer);
        Task UpdateCustomer(CustomerModel customer);
    }
    public class CustomerRepository(AppDbContext AppDbContext) : ICustomerRepository
    {
        public async Task<CustomerModel> CreateCustomer(CustomerModel customer)
        {

            AppDbContext.Customers.Add(customer);
            await AppDbContext.SaveChangesAsync();
            return customer;

        }

        public Task<bool> CustomerModelExist(long idCustomer)
        {
            return AppDbContext.Customers.AnyAsync(c => c.ID == idCustomer);
        }

        public Task<CustomerModel> GetCustomer(long idCustomer)
        {
            return AppDbContext.Customers
                    .Include(u => u.IdentificationType)
                    .Include(u => u.District)
                    .Include(u => u.District!.Canton)
                    .Include(u => u.District!.Canton!.Province)
                    .Include(u => u.District!.Region)
                    .FirstOrDefaultAsync(c => c.ID == idCustomer)!;
        }

        public Task<List<CustomerModel>> GetCustomers()
        {
            return AppDbContext.Customers
                 .Include(u => u.IdentificationType)
                 .Include(u => u.District)
                 .Include(u => u.District!.Canton)
                 .Include(u => u.District!.Canton!.Province)
                 .Include(u => u.District!.Region)
                 .ToListAsync();
        }

        public async Task UpdateCustomer(CustomerModel customer)
        {
            AppDbContext.Customers.Update(customer);
            await AppDbContext.SaveChangesAsync();
        }
    }
}
