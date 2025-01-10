using AppWebGenesisFE.BL.Repositories;
using AppWebGenesisFE.Models.Entities.Customer;
using AppWebGenesisFE.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.BL.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerModel>> GetCustomers();
        Task<CustomerModel> GetCustomer(long idCustomer);
        Task<CustomerModel> CreateCustomer(CustomerModel customer);
        Task<bool> CustomerModelExist(long idCustomer);
        Task UpdateCustomer(CustomerModel customer);


    }

    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        public Task<CustomerModel> CreateCustomer(CustomerModel customer)
        {
            return customerRepository.CreateCustomer(customer);
        }

        public Task<bool> CustomerModelExist(long idCustomer)
        {
            return customerRepository.CustomerModelExist(idCustomer);
        }

        public Task<CustomerModel> GetCustomer(long idCustomer)
        {
            return customerRepository.GetCustomer(idCustomer);
        }

        public Task<List<CustomerModel>> GetCustomers()
        {
            return customerRepository.GetCustomers();
        }

        public Task UpdateCustomer(CustomerModel customer)
        {
            return customerRepository.UpdateCustomer(customer);
        }
    }
}
