using AppWebGenesisFE.BL.Services;
using AppWebGenesisFE.Models.Models.Auth;
using AppWebGenesisFE.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppWebGenesisFE.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponseModel>> Login(LoginModel model)
        {
            try
            {
                var response = await _authService.LoginAsync(model);
                return Ok(new BaseResponseModel
                {
                    Success = true,
                    Data = response
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Ok(new BaseResponseModel
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseResponseModel>> Register(RegisterTenantModel model)
        {
            try
            {
                var response = await _authService.RegisterTenantAsync(model);
                return Ok(new BaseResponseModel
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponseModel
                {
                    Success = false,
                    ErrorMessage = "Error al registrar la empresa"
                });
            }
        }
    }
}
