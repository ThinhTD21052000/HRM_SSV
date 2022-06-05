using Domain.Modals.MonthTimekeeping;
using Domain.Modals.Timekeeping;
using Domain.Modals.User;
using HRMClient.Services;
using Microsoft.AspNetCore.Components;

namespace HRMClient.Pages.Client
{
    public partial class MonthTimeKeeping
    {
        [Inject] private IMonthTimekeepingService _monthTimekeepingService { get; set; }
        [Inject] private ITimekeepingService _timekeepingService { get; set; }
        [Inject] private IUserService _userService { get; set; }
        private bool Loadding { get; set; } = false;
        private bool LoaddingTimekeeping { get; set; } = false;
        private bool VisibleModal { get; set; } = false;
        private bool VisibleModalTimekeeping { get; set; } = false;
        private string ModelTitle { get; set; } = string.Empty;
        private List<MonthTimekeepingToGet> MonthTimekeepingList { get; set; } = new();
        private List<UserToGet> UserList { get; set; } = new();
        private List<Status> StatusList { get; set; } = new List<Status>();
        private List<TimekeepingToGet> TimekeepingList { get; set; } = new();
        private List<TimekeepingToGet> TimekeepingList2 { get; set; } = new();
        private MonthTimekeepingToGet MonthTimekeepingToGet { get; set; } = new();
        private TimekeepingToGet TimekeepingToGet { get; set; } = new();
        public int MTId { get; set; } = 0;
        private Status status { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await LoadUser();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            var list = new List<Status>();
            list.Add(new Status { Id = 1, Title = "Đi làm" });
            list.Add(new Status { Id = 2, Title = "Nghỉ phép" });
            list.Add(new Status { Id = 3, Title = "Nghỉ không phép" });
            list.Add(new Status { Id = 4, Title = "Đi muộn" });
            StatusList.AddRange(list);
            Loadding = false;
        }
        private async Task LoadData()
        {
            Loadding = true;
            MonthTimekeepingList = await _monthTimekeepingService.GetList();
           
            StateHasChanged();
        }
        private async Task LoadUser()
        {
            var list = await _userService.GetList();
            UserList = list.Where(x => !x.RoleName.Equals("admin")).ToList();
            
        }
        private void OpenInsert()
        {
            ModelTitle = "Thêm lịch chấm công";
            VisibleModal = true;
            StateHasChanged();
        }
        private async Task Insert()
        {
            Loadding = true;
            MonthTimekeepingToGet.CreateDate = DateTime.Now;
            MonthTimekeepingToGet.Status = 1;
            await _monthTimekeepingService.Add(Mapper.Map<MonthTimekeepingToAdd>(MonthTimekeepingToGet));
            Loadding = false;
            VisibleModal = false;
            await LoadData();
        }
        private void HideModal()
        {
            VisibleModal = false;
        }
        private void HideModalTimekeeping()
        {
            VisibleModalTimekeeping = false;
        }
        private async Task OpenTimeKeeping(int id)
        {
            LoaddingTimekeeping = true;
            TimekeepingList = await _timekeepingService.GetList(id);
            MTId = id;
            LoaddingTimekeeping = false;
            VisibleModal = true;
            StateHasChanged();
        }
        private async Task Upsert() 
        {
            var list = new List<TimekeepingToGet>();
            for (int i = 0; i < UserList.Count(); i++)
                list.Add(new TimekeepingToGet { Date = TimekeepingToGet.Date, UserId = UserList[0].Id.ToString(), MTKId = MTId });
            foreach (var item in list)
            {
                await _timekeepingService.Add(Mapper.Map<TimekeepingToAdd>(item));
            }
            TimekeepingList = await _timekeepingService.GetList(MTId);
        }
        private async Task Save() 
        {
            foreach (var item in TimekeepingList2)
            {
                await _timekeepingService.Update(Mapper.Map<TimekeepingToUpdate>(item));
            }
            TimekeepingList = await _timekeepingService.GetList(MTId);
        }
        private async Task ShowTimeKeeping(DateTime Date) 
        {
            LoaddingTimekeeping = true;
            TimekeepingList2 = await _timekeepingService.GetListByDate(Date);
            LoaddingTimekeeping = false;
            VisibleModalTimekeeping = true;
            StateHasChanged();
        }
        private async Task Delete(int Id) 
        {
            Loadding = true;
            await _monthTimekeepingService.Delete(Id);
            Loadding = false;
            await LoadData();
        }
        private Task OnSelectedItemChangedHandler(Status status, int I) 
        {
            TimekeepingList2[I].Status = status.Id;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
