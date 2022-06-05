using AntDesign;
using Client.Services;
using Domain.Modals.Team;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Page.Admin
{
    public partial class Team
    {
        [Inject] private ITeamService _teamService { get; set; }
        [Inject] private IUserService _userService { get; set; }
        private bool Loadding { get; set; } = false;
        private bool CheckUpsert { get; set; } = false;
        private bool VisibleModal { get; set; } = false;
        private string ModelTitle { get; set; } = string.Empty;
        private List<TeamToGet> TeamList { get; set; } = new();
        private TeamToGet TeamToGet { get; set; } = new();
        private List<string> Options { get; set; } = new List<string>();
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            Loadding = true;
            var users = await _userService.GetList();
            TeamList = await _teamService.GetList();
            foreach (var item in users)
            {
                if (!item.RoleName.ToLower().Equals("admin"))
                    Options.Add(item.LastName + item.FirstName);
            }
            Loadding = false;
            StateHasChanged();
        }
        private void OpenUpsert(bool Status, TeamToGet Team)
        {
            CheckUpsert = Status;
            TeamToGet = Team;
            ModelTitle = Status ? "Sửa thông tin đội" : "Thêm thông tin đội";
            VisibleModal = true;
            StateHasChanged();

        }
        private async Task Upsert()
        {
            if (CheckUpsert)
            {
                Loadding = true;
                await _teamService.Update(Mapper.Map<TeamToUpdate>(TeamToGet));
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
                await _teamService.Add(Mapper.Map<TeamToAdd>(TeamToGet));
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
            await _teamService.Delete(Id);
            Loadding = false;
            await LoadData();
        }

        private void HideModal()
        {
            VisibleModal = false;
        }
    }
}
