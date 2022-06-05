using AntDesign;
using HRMClient.Services;
using Domain.Modals.Position;
using Microsoft.AspNetCore.Components;

namespace HRMClient.Pages.Admin
{
    public partial class Position
    {
        [Inject] private IPositionService _positionService { get; set; }
        private bool Loadding { get; set; } = false;
        private bool CheckUpsert { get; set; } = false;
        private bool VisibleModal { get; set; } = false;
        private string ModelTitle { get; set; } = string.Empty;
        private List<PositionToGet> PositionList { get; set; } = new();
        private PositionToGet PositionToGet { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            Loadding = true;
            PositionList = await _positionService.GetList();
            Loadding = false;
            StateHasChanged();
        }
        private void OpenUpsert(bool Status, PositionToGet Position)
        {
            CheckUpsert = Status;
            PositionToGet = Position;
            ModelTitle = Status ? "Sửa thông tin phòng ban" : "Thêm thông tin phòng ban";
            VisibleModal = true;
            StateHasChanged();

        }
        private async Task Upsert()
        {
            if (CheckUpsert)
            {
                Loadding = true;
                await _positionService.Update(Mapper.Map<PositionToUpdate>(PositionToGet));
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
                PositionToGet.Id = 0;
                await _positionService.Add(Mapper.Map<PositionToAdd>(PositionToGet));
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

        private async Task Delete(int id)
        {
            Loadding = true;
            await _positionService.Delete(id);
            Loadding = false;
            await LoadData();
        }

        private void HideModal()
        {
            VisibleModal = false;
        }
    }
}
