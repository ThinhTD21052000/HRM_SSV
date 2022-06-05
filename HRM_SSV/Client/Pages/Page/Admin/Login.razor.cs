using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Page.Admin
{
    public partial class Login
    {
        [Inject] private IAuthService _authService { get; set; }
        private string UserName { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty; 
        private bool Loadding { get; set; } = false;
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
        public async Task HandelLogin()
        {
            Loadding = true;
            var result = await _authService.Login(UserName, Password);
            Loadding = false;
            if (result.IsSuccess)
            {
                await LocalStorage.SetItemAsync("user", result.User);
                NavigationManager.NavigateTo("/");
            }
            else
                ToastService.ShowError("Sai tên tài khoản hoặc mật khẩu", "Lỗi!");
        }
    }
}
