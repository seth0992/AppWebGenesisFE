using AppWebGenesisFE.BL.Services;
using AppWebGenesisFE.Models.Entities.Customer;
using AppWebGenesisFE.Models.Models;
using AppWebGenesisFE.Models.Models.Customer;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace AppWebGenesisFE.ApiService.Controllers
{
    [Authorize] // Requiere autenticación
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService customerService, IMapper mapper) : ControllerBase
    {

  
        [HttpGet]
        public async Task<ActionResult<BaseResponseModel>> GetCustomers()
        {

            try
            {
                var customers = await customerService.GetCustomers();
                return Ok(new BaseResponseModel { Success = true, Data = customers });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponseModel { Success = false, ErrorMessage = ex.Message });
            }

        }

        [HttpPost]
        public async Task<ActionResult<BaseResponseModel>> CreateCustomer(CustomerDTO modeldDTO)
        {
            var model = mapper.Map<CustomerModel>(modeldDTO);

            // Create product
            await customerService.CreateCustomer(model);
            return Ok(new BaseResponseModel { Success = true });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponseModel>> GetCustomer(long id)
        {
            var product = await customerService.GetCustomer(id);
            if (product == null)
            {
                return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Customer not found" });
            }
            return Ok(new BaseResponseModel { Success = true, Data = product });
        }

        [HttpPut("{idCustomer}")]
        public async Task<ActionResult<BaseResponseModel>> UpdateCustomer(long idCustomer, CustomerModel modeldDTO)
        {
            var model = mapper.Map<CustomerModel>(modeldDTO);

            if (idCustomer != model.ID || !await customerService.CustomerModelExist(idCustomer))
            {
                return Ok(new BaseResponseModel { Success = false, ErrorMessage = "Customer not found" });
            }
            // Update custoemr
            await customerService.UpdateCustomer(model);
            return Ok(new BaseResponseModel { Success = true });
        }
    }
}
