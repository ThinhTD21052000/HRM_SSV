using HRMClient.Services;
using Microsoft.AspNetCore.Components;

namespace HRMClient.Pages.Admin
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
                if (result.User.RoleName.ToLower().Equals("admin"))
                {
                    await LocalStorage.SetItemAsync("user", result.User);
                    NavigationManager.NavigateTo("/Admin/Home");
                }
                else
                    ToastService.ShowError("Tài khoản của bạn không có quyền đăng nhập ở đây!", "Lỗi!");
            }
            else
                ToastService.ShowError("Sai tên tài khoản hoặc mật khẩu", "Lỗi!");
        }
    }
}
