using Domain.Modals.Bonus_Wage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Bonus_WageController : ControllerBase
    {
        private readonly IBonus_WageService _bonus_WageService;
        public Bonus_WageController(IBonus_WageService bonus_WageService)
        {
            _bonus_WageService = bonus_WageService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _bonus_WageService.GetList());
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _bonus_WageService.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(Bonus_WageToAdd bonus_Wage)
        {
            await _bonus_WageService.Create(bonus_Wage);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Bonus_WageToUpdate bonus_Wage)
        {
            await _bonus_WageService.Update(bonus_Wage);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bonus_WageService.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
