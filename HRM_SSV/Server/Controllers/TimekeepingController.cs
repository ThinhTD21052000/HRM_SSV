using Domain.Modals.Orther;
using Domain.Modals.Timekeeping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimekeepingController : ControllerBase
    {
        private readonly ITimekeepingService _timekeepingService;
        private readonly IUserService _userService;
        private readonly IPositionService _positionService;
        private readonly ITeamService _teamService;
        private readonly IRoleService _roleService;
        public TimekeepingController(ITimekeepingService timekeepingService, IUserService userService,
            IPositionService positionService, ITeamService teamService, IRoleService roleService)
        {
            _timekeepingService = timekeepingService;
            _userService = userService;
            _positionService = positionService;
            _teamService = teamService;
            _roleService = roleService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList(int id)
        {
            var list = await _timekeepingService.GetList();
            if(list != null)
            {
                list = list.Where(x => x.MTKId == id).ToList();
                list = list.GroupBy(x => x.Date).Select(y => y.First()).ToList();
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("GetListByDate")]
        public async Task<IActionResult> GetListByDate(DateTime date)
        {
            var list = await _timekeepingService.GetList();
            if(list != null)
            {
                list = list.Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year).ToList();
                foreach (var item in list)
                {
                    var user = await _userService.Get(Guid.Parse(item.UserId));
                    var position = await _positionService.Get(user.PositionId);
                    var team = await _teamService.Get(user.TeamId);
                    item.FullName = $"{user.LastName} {user.FirstName}";
                    item.PositionName = position.Name;
                    item.TeamName = team.Name;
                }
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("GetListWorkingDay")]
        public async Task<IActionResult> GetListWorkingDay(int month, int year)
        {
            var list = await _timekeepingService.GetList();
            List<Payroll> payrolls = new();
            var userList = await _userService.GetList();
            int workingDay = 0, late = 0, pairLeave = 0, unpairLeave = 0;
            userList = userList.Where(x => !x.RoleName.ToLower().Equals("admin")).ToList();
            if (list != null)
            {
                list = list.Where(x => x.Date.Month == month && x.Date.Year == year).ToList();
                foreach (var item in list)
                {
                    var user = await _userService.Get(Guid.Parse(item.UserId));
                    var position = await _positionService.Get(user.PositionId);
                    var team = await _teamService.Get(user.TeamId);
                    item.FullName = $"{user.LastName} {user.FirstName}";
                    item.PositionName = position.Name;
                    item.TeamName = team.Name;
                }

                foreach (var item in userList)
                {
                    var tempList = list.Where(x => x.UserId.Equals(item.Id.ToString())).ToList();
                    foreach (var item2 in tempList)
                    {
                        switch (item2.Status)
                        {
                            case 1: workingDay += 1; break;
                            case 2: pairLeave += 1; break;
                            case 3: unpairLeave += 1; break;
                            case 4: unpairLeave += 1; break;
                        }
                    }
                }
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _timekeepingService.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(TimekeepingToAdd timekeeping)
        {
            var list = await _timekeepingService.GetList();
            if(list != null)
            {
                var item = list.FirstOrDefault(x=>x.Date == timekeeping.Date);
                if(item != null)
                {
                    return Ok("Insert False");
                }
            }
            await _timekeepingService.Create(timekeeping);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(TimekeepingToUpdate timekeeping)
        {
            await _timekeepingService.Update(timekeeping);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _timekeepingService.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
