using Domain.Modals.ViolationMoney;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationMoneyController : ControllerBase
    {
        private readonly IViolationMoneyService _violationmoneyservice;
        public ViolationMoneyController(IViolationMoneyService violationMoneyService)
        {
            _violationmoneyservice = violationMoneyService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _violationmoneyservice.GetList());
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _violationmoneyservice.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(ViolationMoneyToAdd violationMoney)
        {
            await _violationmoneyservice.Create(violationMoney);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(ViolationMoneyToUpdate violationMoney)
        {
            await _violationmoneyservice.Update(violationMoney);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _violationmoneyservice.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
