using AppWebGenesisFE.Models.Models.Auth;

namespace AppWebGenesisFE.Web.Components.Pages.Login
{
    public partial class Login
    {
        private LoginModel loginModel = new LoginModel();
        private async Task HandleLogin()
        {
            var res = await ApiClient.PostAsync<AuthResponse, LoginModel>("/api/auth/login", loginModel);
            if (res != null && res.Token != null)
            {
                await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAsAuthenticated(res);
                Navigation.NavigateTo("/");
            }
        }
    }
}
