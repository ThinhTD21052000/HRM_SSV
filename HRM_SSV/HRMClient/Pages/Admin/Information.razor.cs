using HRMClient.Services;
using Domain.Modals.Response;
using Domain.Modals.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HRMClient.Pages.Admin
{
    public partial class Information
    {
        [Inject] private IUserService _userService { get; set; }
        string ActiveKey { get; set; } = "1";
        private List<Sex> SexList { get; set; } = new();
        public UserToGet UserToGet { get; set; } = new();
        public UserToGet UserClone { get; set; } = new();
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string RecentPassword { get; set; } = string.Empty;
        private bool Loadding { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            UserClone = UserToGet = await LocalStorage.GetItemAsync<UserToGet>("user");
            SexList = new();
            SexList.Add(new Sex { Id = 1, Name = "Nam" });
            SexList.Add(new Sex { Id = 2, Name = "Nữ" });
            SexList.Add(new Sex { Id = 3, Name = "Khác" });
            StateHasChanged();
        }
        private async Task Update()
        {
            Loadding = true;
            await _userService.Update(Mapper.Map<UserToUpdate>(UserToGet));
            UserToGet = await _userService.Get(UserToGet.Id);
            UserClone = UserToGet;
            await LocalStorage.SetItemAsync("user", UserToGet);
            Loadding = false;
            ToastService.ShowSuccess("Sửa thông tin thành công", "Thành công!");
            StateHasChanged();
        }
        private async Task ChangePassword()
        {
            Loadding = true;
            bool check = await _userService.CheckPassword(UserToGet.Id, CurrentPassword);
            if (check)
            {
                if (NewPassword.Equals(RecentPassword))
                {
                    Loadding = true;
                    await _userService.ChangePassword(new ChangePasswordReponse { User = UserToGet, OldPassword = CurrentPassword, NewPassword = NewPassword });
                    Loadding = false;
                    ToastService.ShowSuccess("Đổi mật khẩu thành công", "Thành công!");
                }
                else
                    ToastService.ShowError("Mật khẩu mới nhập lại không khớp", "Lỗi!");
            }
            else
                ToastService.ShowError("Mật khẩu nhập cũ không đúng", "Lỗi!");
            Loadding = false;
            StateHasChanged();
        }
        private void Cancel()
        {
            UserToGet = UserClone;
            StateHasChanged();
        }
        private async Task FilePicker(InputFileChangeEventArgs eventArgs)
        {
            IBrowserFile file = eventArgs.File;
            Loadding = true;
            Stream stream = file.OpenReadStream(100000000);
            MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            stream.Close();
            UserToGet.Avatar = ms.ToArray();
            UserToGet.ImageFile = Convert.ToBase64String(UserToGet.Avatar); 
            Loadding = false;
            //await JSRuntime.InvokeVoidAsync("onFilePicked", "imageFile", base64String);
            StateHasChanged();
        }
    }
}
