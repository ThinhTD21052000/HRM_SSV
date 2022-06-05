using AntDesign;
using Client.Services;
using Domain.Modals.Role;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Page.Admin
{
    public partial class Role
    {
        [Inject] private IRoleService _RoleService { get; set; }
        private bool Loadding { get; set; } = false;
        private bool CheckUpsert { get; set; } = false;
        private bool VisibleModal { get; set; } = false;
        private string ModelTitle { get; set; } = string.Empty;
        private List<RoleToGet> RoleList { get; set; } = new();
        private RoleToGet RoleToGet { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            Loadding = true;
            RoleList = await _RoleService.GetList();
            Loadding = false;
            StateHasChanged();
        }
        private void OpenUpsert(bool Status, RoleToGet Role)
        {
            CheckUpsert = Status;
            RoleToGet = Role;
            ModelTitle = Status ? "Sửa thông tin quyền hạn" : "Thêm thông tin quyền hạn";
            VisibleModal = true;
            StateHasChanged();

        }
        private async Task Upsert()
        {
            if (CheckUpsert)
            {
                Loadding = true;
                await _RoleService.Update(Mapper.Map<RoleToUpdate>(RoleToGet));
                Loadding = false;
                await Notice.Success(new NotificationConfig()
                {
                    Message = "Thành công",
                    Description = "Sửa dữ liệu thành công!",
                    Placement = NotificationPlacement.BottomRight
                });
            }
            else
            {
                Loadding = true;
                await _RoleService.Add(Mapper.Map<RoleToAdd>(RoleToGet));
                Loadding = false;
                await Notice.Success(new NotificationConfig()
                {
                    Message = "Thành công",
                    Description = "Thêm dữ liệu thành công!",
                    Placement = NotificationPlacement.BottomRight
                });
            }
            VisibleModal = false; 
            await LoadData();
        }

        private async Task Delete(Guid Id)
        {
            Loadding = true;
            await _RoleService.Delete(Id); 
            Loadding = false;
            await LoadData();
        }

        private void HideModal()
        {
            VisibleModal = false;
        }
    }
}
