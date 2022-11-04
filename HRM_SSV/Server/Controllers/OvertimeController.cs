using Domain.Modals.Overtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeController : ControllerBase
    {
        private readonly IOvertimeService _overtimeService;
        public OvertimeController(IOvertimeService overtimeservice)
        {
            _overtimeService = overtimeservice;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _overtimeService.GetList());
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _overtimeService.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(OvertimeToAdd overtime)
        {
            await _overtimeService.Create(overtime);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(OvertimeToUpdate overtime)
        {
            await _overtimeService.Update(overtime);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _overtimeService.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
