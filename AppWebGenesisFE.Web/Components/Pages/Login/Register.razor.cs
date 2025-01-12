using AppWebGenesisFE.Models.Models.Auth;
using AppWebGenesisFE.Models.Models;

namespace AppWebGenesisFE.Web.Components.Pages.Login
{
    public partial class Register
    {
        private RegisterTenantModel registerModel = new() { Rol = "Admin" };

        private async Task HandleRegister()
        {
            try
            {
                var response = await ApiClient.PostAsync<BaseResponseModel, RegisterTenantModel>("/api/auth/register", registerModel);

                if (response.Success)
                {
                    ToastService.ShowSuccess("Registro exitoso");
                    Navigation.NavigateTo("/login");
                }
                else
                {
                    ToastService.ShowError(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error during registration");
                ToastService.ShowError("Error al intentar registrar la empresa");
            }
        }
    }
}
