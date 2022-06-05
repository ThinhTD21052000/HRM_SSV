using Domain.Modals.LaborContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LaborContractController : ControllerBase
    {
        private readonly ILaborContractService _laborContractService;
        public LaborContractController(ILaborContractService laborContractService)
        {
            _laborContractService = laborContractService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList(Guid id)
        {
            var list = await _laborContractService.GetList();
            var laborContracts = list.Where(x=>x.UserId.Equals(id.ToString())).ToList();
            return Ok(laborContracts);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _laborContractService.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(LaborContractToAdd laborContract)
        {
            await _laborContractService.Create(laborContract);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(LaborContractToUpdate laborContract)
        {
            await _laborContractService.Update(laborContract);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _laborContractService.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
