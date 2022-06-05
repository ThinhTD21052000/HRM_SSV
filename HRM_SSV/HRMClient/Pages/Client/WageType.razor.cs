using Domain.Modals.Position;
using Domain.Modals.WageType;
using HRMClient.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HRMClient.Pages.Client
{
    public partial class WageType
    {
        [Inject] private IWageTypeService _wageTypeService { get; set; }
        [Inject] private IPositionService _positionService { get; set; }
        private bool Loadding { get; set; } = false;
        private bool CheckUpsert { get; set; } = false;
        private bool VisibleModal { get; set; } = false;
        private string ModelTitle { get; set; } = string.Empty;
        private List<Wage_TypeToGet> WageTypeList { get; set; } = new();
        private List<PositionToGet> PositionList { get; set; } = new();
        private Wage_TypeToGet WageTypeToGet { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            PositionList = await _positionService.GetList();

        }
        private async Task LoadData()
        {
            Loadding = true;
            WageTypeList = await _wageTypeService.GetList();
            Loadding = false;
            StateHasChanged();
        }
        private void OpenUpsert(bool Status, Wage_TypeToGet WageType)
        {
            CheckUpsert = Status;
            WageTypeToGet = WageType;
            ModelTitle = Status ? "Sửa thông tin phòng ban" : "Thêm thông tin phòng ban";
            VisibleModal = true;
            StateHasChanged();

        }
        private async Task Upsert()
        {
            if (CheckUpsert)
            {
                Loadding = true;
                await _wageTypeService.Update(Mapper.Map<Wage_TypeToUpdate>(WageTypeToGet));
                Loadding = false;
                ToastService.ShowSuccess("Sửa Lương cứng thành công!", "Thành công!");
            }
            else
            {
                Loadding = true;
                WageTypeToGet.Id = 0;
                await _wageTypeService.Add(Mapper.Map<Wage_TypeToAdd>(WageTypeToGet));
                Loadding = false;
                ToastService.ShowSuccess("Thêm Lương cứng thành công!", "Thành công!");
            }
            VisibleModal = false;
            await LoadData();
        }

        private async Task Delete(int id)
        {
            Loadding = true;
            await _wageTypeService.Delete(id);
            Loadding = false;
            await LoadData();
        }

        private void HideModal()
        {
            VisibleModal = false;
        }
        private async Task FilePicker(InputFileChangeEventArgs eventArgs)
        {
            IBrowserFile file = eventArgs.File;
            Stream stream = file.OpenReadStream(100000000);
            MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            stream.Close();
            WageTypeToGet.Attachment = ms.ToArray();
            StateHasChanged();
        }
    }
}
