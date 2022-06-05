using Domain.Modals.MonthlySalary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlySalaryController : ControllerBase
    {
        private readonly IMonthlySalaryService _monthlySalaryService;
        public MonthlySalaryController(IMonthlySalaryService monthlySalaryService)
        {
            _monthlySalaryService = monthlySalaryService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            var list = await _monthlySalaryService.GetList();
            list = list.OrderByDescending(x => x.Year).ToList();
            return Ok();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _monthlySalaryService.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(MonthlySalaryToAdd monthlySalary)
        {
            await _monthlySalaryService.Create(monthlySalary);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(MonthlySalaryToUpdate monthlySalary)
        {
            await _monthlySalaryService.Update(monthlySalary);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _monthlySalaryService.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
