using AppWebGenesisFE.BL.Services;
using AppWebGenesisFE.Models.Entities.Customer;
using AppWebGenesisFE.Models.Exceptions;
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
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponseModel>> GetCustomer(long id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                throw new NotFoundException("Cliente", id);
            }
            return Ok(new BaseResponseModel { Success = true, Data = customer });
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponseModel>> CreateCustomer(CustomerDTO modelDTO)
        {
            try
            {
                // Validación de modelo
                if (string.IsNullOrWhiteSpace(modelDTO.CustomerName))
                {
                    var errors = new Dictionary<string, string[]>
                {
                    { "CustomerName", new[] { "El nombre del cliente es requerido" } }
                };
                    throw new ValidationException(errors);
                }

                var model = _mapper.Map<CustomerModel>(modelDTO);
                await _customerService.CreateCustomer(model);
                return Ok(new BaseResponseModel { Success = true });
            }
            catch (Exception ex) when (ex is not ValidationException && ex is not NotFoundException)
            {
                throw new ApiException("Error al crear el cliente", ex);
            }
        }

        [HttpPut("{idCustomer}")]
        public async Task<ActionResult<BaseResponseModel>> UpdateCustomer(long idCustomer, CustomerModel model)
        {
            if (idCustomer != model.ID)
            {
                throw new ValidationException("El ID del cliente no coincide con el ID de la ruta");
            }

            if (!await _customerService.CustomerModelExist(idCustomer))
            {
                throw new NotFoundException("Cliente", idCustomer);
            }

            await _customerService.UpdateCustomer(model);
            return Ok(new BaseResponseModel { Success = true });
        }
    }
}
