using AppWebGenesisFE.Models.Models;
using AppWebGenesisFE.Models.Models.Auth;
using AppWebGenesisFE.Web.Authentication;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace AppWebGenesisFE.Web.Components.Pages.Login
{
    //public partial class Login
    //{
    //    //private LoginModel loginModel = new LoginModel();
    //    //private async Task HandleLogin()
    //    //{
    //    //    var res = await ApiClient.PostAsync<AuthResponse, LoginModel>("/api/auth/login", loginModel);
    //    //    if (res != null && res.Token != null)
    //    //    {
    //    //        await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAsAuthenticated(res);
    //    //        Navigation.NavigateTo("/");
    //    //    }
    //    //}
    //}

    public partial class Login
    {
        private LoginModel loginModel = new();

        [Inject]
        private IToastService toastService { get; set; }

        private async Task HandleLogin()
        {
            try
            {
                var response = await ApiClient.PostAsync<BaseResponseModel, LoginModel>("/api/auth/login", loginModel);

                if (response.Success && response.Data != null)
                {
                    var authResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Data.ToString()!);

                    if (authResponse != null)
                    {
                        var userSession = new UserSession
                        {
                            //Email = authResponse.User!.Email,
                            //Role = authResponse.User.Rol,
                            //TenantId = authResponse.User.TenantId,
                            Token = authResponse.Token,
                            //Expiration = authResponse.Expiration
                        };

                        await ((CustomAuthStateProvider)AuthStateProvider).UpdateAuthenticationState(userSession);
                        Navigation.NavigateTo("/");
                    }
                }
                else
                {
                    ToastService.ShowError(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error during login");
                toastService.ShowError("Error al intentar iniciar sesión");
            }
        }
    }
}
