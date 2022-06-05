using AntDesign;
using Client.Services;
using Domain.Modals.Company;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Pages.Page.Admin
{
    public partial class Company
    {
        [Inject] private ICompanyService _companyService { get; set; }
        private bool Loadding { get; set; } = false;
        private bool CheckUpsert { get; set; } = false;
        private CompanyToGet CompanyToGet { get; set; } = new();
        private string ImageBase64 { get; set; } = "iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAALVBMVEX////CwsLh4eG9vb3w8PD5+fni4uLt7e3n5+f7+/v19fXl5eXq6ury8vLd3d1IURIqAAAEM0lEQVR4nO2dgbKqIBRFuyUqpv3/5770IAcxJ1Owtm+vmTvzlFTWO3AETbv8nZ3LtyuQnd7wdl7E8HY5LzcawkNDfGiIDw3xoSE+NMSHhvjQEB8a4kNDfGiIDw3xoSE+NMSHhvjQEB8aekzxDpu+dm8Pat7vY7VhcX3DY8XRPuXtQYv3+0hneKXhBmhIwx4arj9Yllz6+ILh/mongYYKDT00PBgaKjT00PBgaKjQ0EPDg6GhQkMPDQ+GhgoNPTQ8GBoqNPTQ8GByGMrl58f+yiXB1SaloTXC/solwdVmxX1n3sfHh4b40BAfGuJDQ3xoiA8N8aEhPjTEh4b40BAfGuJDQ3xoiA8N19HWT7oXBbbpi6rmRVFTDRvNi0zXb9OlusuVxrDsb+ZVs9W29c+8VNMKm0qfhpk+mmnu4/pyxc3BFeQ07MqrUjZLJdd7oFiFBXWKOGY0jJ6UKrUVN9OSa+0V22ibBIr5DDsXoGefchEbo9jIcln7otqVOEEtSPC1gWyGduhPZdWHx0jbu7si8WiH+DR1IG+CYBspmHfuT8lmKG10TBbVzGMMmxXd4d9tGGpbp2mnuQwlhK1frjWI9bT5FX5RWq/PoJJVdwcxl6GZ9DzXKyUg16m77XvdENHBNUis7TQLbSSXYRXlCTN8pO9h0iyD4YF1f85ICyRX/aph0CoHrG9ycqp40b1s3Cpt1A62kcsw6oZOuV8h7fVFZGwZBfcSR3sTuQxna7xh3Ns885BN0vFWjjes1FDHNg+7aLg3mX7XUIenoIbL/fADw19tpXWU+WVF/5HgHBCMwO2YSwOh2XllE7kMZQCmy1r9YCxgu6LoKm/ow+xYPK98RC7D+Gw9NEiZIz6mobL+k9JDdR9Bj91BtnFptCqobTQuNd5QzpTaEWfn1E1km1sMzdTPDEyQebppAlFDacm+83ZJumE+Q8mTbtwsEXVvP3Ezx7HqjbZnmWa04Q52N9KUhu34VXD5NribrxfPJXfxZfwvGGf/3bOoa69qeHGT/2dB49bvHZUmNQzoo2Xv0crFqzHaLZt4R7t7YS7DIcWbqWI40aujj5fRJZyEgjkNnwlU15fTynahvVzMEWwdbLM7y/SkMYwFxzcqmUpSaj17OZ4tWrkOU7fR1e2mkrll26V5iV8aQxOjRX36sS/rOiSmV0V25bMi6+CdGXxoiA8N8aEhPjTEh4b40BAfGuJDQ3xoiA8N8aEhPjT0jD9qk79Kq8j3Szq/9SYsvs2sh4YeGh4MDRUaemh4MDRUaOih4cHQUKGhh4YHQ0OFhh4aHgwNFRp6aHgwGQ0XOc3vAS9z+t90puEWaEjDnh/PpUcajndIl0n2HM8HB015hxQWGuJDQ3xoiA8N8aEhPjTEh4b40BAfGuJDQ3xoiA8N8aEhPjTEh4b40BCf/8Xw73Ze/sTw3Jzf8B+89zsx8QEiSQAAAABJRU5ErkJggg==";
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            Loadding = true;
            CompanyToGet = await _companyService.GetList();
            if (CompanyToGet.Logo != null)
            {
                CompanyToGet.ImageFile = ImageBase64;
                CheckUpsert = true;
            }
            Loadding = false;
            StateHasChanged();
        }
        private async Task FilePicker(InputFileChangeEventArgs eventArgs)
        {
            IBrowserFile file = eventArgs.File;
            Stream stream = file.OpenReadStream(100000000);
            MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            stream.Close();
            CompanyToGet.Logo = ms.ToArray();
            CompanyToGet.ImageFile = Convert.ToBase64String(CompanyToGet.Logo);
            //await JSRuntime.InvokeVoidAsync("onFilePicked", "imageFile", base64String);
            StateHasChanged();
        }

        private async Task Upsert()
        {
            if (CheckUpsert)
            {
                Loadding = true;
                await _companyService.Update(Mapper.Map<CompanyToUpdate>(CompanyToGet));
                Loadding = false;
                await Notice.Open(new NotificationConfig()
                {
                    Message = "Thông báo",
                    Description = "Sửa dữ liệu công ty thành công!",
                    Placement = NotificationPlacement.BottomRight
                });
                await LoadData();
            }
            else
            {
                CompanyToGet.Logo = Convert.FromBase64String(ImageBase64);
                await Notice.Open(new NotificationConfig()
                {
                    Message = "Thông báo",
                    Description = "Thêm dữ liệu công ty thành công!",
                    Placement = NotificationPlacement.BottomRight
                });
                Loadding = true;
                await _companyService.Add(Mapper.Map<CompanyToAdd>(CompanyToGet));
                Loadding = false;
                await LoadData();
            }
        }
    }
}
