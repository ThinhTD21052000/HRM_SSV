using Domain.Modals.Timekeeping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimekeepingController : ControllerBase
    {
        private readonly ITimekeepingService _timekeepingService;
        public TimekeepingController(ITimekeepingService timekeepingService)
        {
            _timekeepingService = timekeepingService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _timekeepingService.GetList());
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
