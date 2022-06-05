using Aspose.Words;
using Domain.Modals.LaborContract;
using Domain.Modals.User;
using HRMClient.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace HRMClient.Pages.Client
{
    public partial class StaffAttachment
    {
        [Parameter]
        public UserToGet User { get; set; } = new();
        [Inject] private IUserService _userService { get; set; }
        [Inject] private ILaborContractService _laborContractService { get; set; }
        private UserToGet UserToGet { get; set; } = new();
        private List<UserToGet> UserList { get; set; } = new();
        private List<LaborContractToGet> LaborContractList { get; set; } = new();
        private LaborContractToGet LaborContractToGet { get; set; } = new();

        private bool Loadding { get; set; } = false;
        private bool CheckUpsert { get; set; } = false;
        private bool VisibleModal { get; set; } = false;
        private bool VisibleModalView { get; set; } = false;
        private string ModelTitle { get; set; } = string.Empty;
        private string AtachmentBase64 { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            Loadding = true;
            await LoadData();
            Loadding = false;
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Loadding = true;
            await LoadData();
            Loadding = false;
        }

        private async Task LoadData()
        {
            LaborContractList = new();
            Loadding = true;
            LaborContractList = await _laborContractService.GetList(User.Id);
            Loadding = false;
            StateHasChanged();
        }
        private void OpenUpsert(bool Status, LaborContractToGet LaborContractToGet)
        {
            CheckUpsert = Status;
            this.LaborContractToGet = LaborContractToGet;
            ModelTitle = Status ? "Sửa thông tin đội" : "Thêm thông tin đội";
            VisibleModal = true;
            StateHasChanged();

        }
        private async Task Upsert()
        {
            VisibleModal = false;
            if (CheckUpsert)
            {
                LaborContractToUpdate laborContractToUpdate = Mapper.Map<LaborContractToUpdate>(LaborContractToGet);
                laborContractToUpdate.UserId = UserList[0].Id.ToString();
                Loadding = true;
                if (laborContractToUpdate.Attachment.Length > 0)
                {
                    Document loadedFromBytes = new Document(new MemoryStream(laborContractToUpdate.Attachment));
                    MemoryStream pdfStream = new MemoryStream();
                    loadedFromBytes.Save(pdfStream, SaveFormat.Pdf);
                    laborContractToUpdate.AttachmentPDF = pdfStream.ToArray();
                }
                await _laborContractService.Update(laborContractToUpdate);
                Loadding = false;
                ToastService.ShowSuccess("Sửa Tài liệu nhân viên thành công!", "Thành công!");
            }
            else
            {
                LaborContractToAdd laborContractToAdd = Mapper.Map<LaborContractToAdd>(LaborContractToGet);
                laborContractToAdd.UserId = UserList[0].Id.ToString();
                Loadding = true;
                if (laborContractToAdd.Attachment.Length > 0)
                {
                    Document loadedFromBytes = new Document(new MemoryStream(laborContractToAdd.Attachment));
                    MemoryStream pdfStream = new MemoryStream();
                    loadedFromBytes.Save(pdfStream, SaveFormat.Pdf);
                    laborContractToAdd.AttachmentPDF = pdfStream.ToArray();
                }
                await _laborContractService.Add(laborContractToAdd);
                Loadding = false;
                ToastService.ShowSuccess("Tạo Tài liệu nhân viên thành công!", "Thành công!");
            }
            await LoadData();
        }
        private async Task Delete(int Id)
        {
            Loadding = true;
            await _laborContractService.Delete(Id);
            Loadding = false;
            await LoadData();
        }
        private void ViewAtachment(byte[] AttachmentPDF)
        {
            VisibleModalView = true;
            AtachmentBase64 = Convert.ToBase64String(AttachmentPDF);
            StateHasChanged();
        }

        private async Task Download(byte[] Attachment)
        {
            AtachmentBase64 = Convert.ToBase64String(Attachment);
            var fileURL = $"data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,{AtachmentBase64}";
            var fileName = "download.doc";
            await JSRuntime.InvokeVoidAsync("triggerFileDownload", fileName, fileURL);
        }
        private void HideModalView()
        {
            VisibleModalView = false;
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
            LaborContractToGet.Attachment = ms.ToArray();
            StateHasChanged();
        }
    }
}
