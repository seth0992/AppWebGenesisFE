using AppWebGenesisFE.BL.Repositories;
using AppWebGenesisFE.Models.Entities.Customer;
using AppWebGenesisFE.Models.Exceptions;
using AppWebGenesisFE.Models.Models;
using Microsoft.EntityFrameworkCore;
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

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerModel> CreateCustomer(CustomerModel customer)
        {
            try
            {
                return await _customerRepository.CreateCustomer(customer);
            }
            catch (DbUpdateException ex)
            {
                throw new ApiException("Error al guardar el cliente en la base de datos", ex);
            }
        }

        public Task<bool> CustomerModelExist(long idCustomer)
        {
            return _customerRepository.CustomerModelExist(idCustomer);
        }

        public async Task<CustomerModel> GetCustomer(long idCustomer)
        {
            var customer = await _customerRepository.GetCustomer(idCustomer);
            if (customer == null)
            {
                throw new NotFoundException("Cliente", idCustomer);
            }
            return customer;
        }

        public Task<List<CustomerModel>> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        public Task UpdateCustomer(CustomerModel customer)
        {
            return _customerRepository.UpdateCustomer(customer);
        }
    }
}
