using Domain.Modals.Position;
using Domain.Modals.Team;
using Domain.Modals.User;
using HRMClient.Services;
using Microsoft.AspNetCore.Components;

namespace HRMClient.Pages.Client
{
    public partial class Staff
    {
        [Inject] private IUserService _userService { get; set; }
        [Inject] private IPositionService _positionService { get; set; }
        [Inject] private ITeamService _teamService { get; set; }
        [Inject] private IRoleService _roleService { get; set; }
        private List<UserToGet> UserList { get; set; } = new();
        private List<PositionToGet> PositionList { get; set; } = new();
        private List<TeamToGet> TeamList { get; set; } = new();
        private List<Sex> SexList { get; set; } = new();
        private bool Loadding { get; set; } = false;
        private bool VisibleModal { get; set; } = false;
        private bool VisibleModal2 { get; set; } = false;
        private bool CheckUpsert { get; set; } = false;
        private UserToGet UserToGet { get; set; } = new();
        private string ModelTitle { get; set; } = string.Empty;
        public UserToGet UserToStranfer { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            Loadding = true;
            UserList = await _userService.GetList();
            TeamList = await _teamService.GetList();
            PositionList = await _positionService.GetList();
            SexList = new();
            SexList.Add(new Sex { Id = 1, Name = "Nam" });
            SexList.Add(new Sex { Id = 2, Name = "Nữ" });
            SexList.Add(new Sex { Id = 3, Name = "Khác" });
            Loadding = false;
            StateHasChanged();
        }

        private void OpenUpsert(UserToGet User)
        {
            UserToGet = User;
            ModelTitle = "Sửa thông tin nhân viên";
            VisibleModal = true;
            StateHasChanged();

        }
        private async Task Upsert()
        {
            Loadding = true;
            await _userService.Update(Mapper.Map<UserToUpdate>(UserToGet));
            Loadding = false;
            VisibleModal = false;
            await LoadData();
        }

        private void HideModal()
        {
            VisibleModal = false;
        }
        private void HideModal2()
        {
            VisibleModal2 = false;
        }
        private void Open(UserToGet userToGet)
        {
            UserToStranfer = userToGet;
            VisibleModal2 = true;
            StateHasChanged();
        }
    }
    public class Sex
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
