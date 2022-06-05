using Domain.Modals.MonthTimekeeping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonthTimekeepingController : ControllerBase
    {
        private readonly IMonthTimekeepingService _MonthTimekeepingService;
        public MonthTimekeepingController(IMonthTimekeepingService MonthTimekeepingService)
        {
            _MonthTimekeepingService = MonthTimekeepingService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _MonthTimekeepingService.GetList());
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _MonthTimekeepingService.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(MonthTimekeepingToAdd MonthTimekeeping)
        {
            var list = await _MonthTimekeepingService.GetList();
            if (list != null)
            {
                var item = list.FirstOrDefault(x => x.Month == MonthTimekeeping.Month && x.Year == MonthTimekeeping.Year);
                if (item != null)
                {
                    return Ok("Insert False");
                }
            }
            await _MonthTimekeepingService.Create(MonthTimekeeping);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(MonthTimekeepingToUpdate MonthTimekeeping)
        {
            await _MonthTimekeepingService.Update(MonthTimekeeping);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _MonthTimekeepingService.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
