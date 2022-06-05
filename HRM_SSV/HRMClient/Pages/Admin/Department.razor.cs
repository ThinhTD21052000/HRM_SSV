using AntDesign;
using HRMClient.Services;
using Domain.Modals.Department;
using Microsoft.AspNetCore.Components;

namespace HRMClient.Pages.Admin
{
    public partial class Department
    {
        [Inject] private IDepartmentService _departmentService { get; set; }
        private bool Loadding { get; set; } = false;
        private bool CheckUpsert { get; set; } = false;
        private bool VisibleModal { get; set; } = false;
        private string ModelTitle { get; set; } = string.Empty;
        private List<DepartmentToGet> DepartmentList { get; set; } = new();
        private DepartmentToGet DepartmentToGet { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            Loadding = true;
            DepartmentList = await _departmentService.GetList();
            Loadding = false;
            StateHasChanged();
        }
        private void OpenUpsert(bool Status, DepartmentToGet Department)
        {
            CheckUpsert = Status;
            DepartmentToGet = Department;
            ModelTitle = Status ? "Sửa thông tin phòng ban" : "Thêm thông tin phòng ban";
            VisibleModal = true;
            StateHasChanged();

        }
        private async Task Upsert()
        {
            if (CheckUpsert)
            {
                Loadding = true;
                await _departmentService.Update(Mapper.Map<DepartmentToUpdate>(DepartmentToGet));
                Loadding = false;
                await Notice.Open(new NotificationConfig()
                {
                    Message = "Thông báo",
                    Description = "Sửa dữ liệu công ty thành công!",
                    Placement = NotificationPlacement.BottomRight
                });
            }
            else
            {
                Loadding = true;
                DepartmentToGet.Id = 0;
                await _departmentService.Add(Mapper.Map<DepartmentToAdd>(DepartmentToGet));
                Loadding = false;
                await Notice.Open(new NotificationConfig()
                {
                    Message = "Thông báo",
                    Description = "Thêm dữ liệu công ty thành công!",
                    Placement = NotificationPlacement.BottomRight
                });
            }
            VisibleModal = false; 
            await LoadData();
        }

        private async Task Delete(int Id)
        {
            Loadding = true;
            await _departmentService.Delete(Id); 
            Loadding = false;
            await LoadData();
        }

        private void HideModal()
        {
            VisibleModal = false;
        }
    }
}
