using Domain.Modals.WageType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Wage_TypeController : ControllerBase
    {
        private readonly IWage_TypeService _wage_typeservice;
        public Wage_TypeController(IWage_TypeService wage_TypeService)
        {
            _wage_typeservice = wage_TypeService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _wage_typeservice.GetList());
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _wage_typeservice.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(Wage_TypeToAdd wage_Type)
        {
            await _wage_typeservice.Create(wage_Type);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Wage_TypeToUpdate wage_Type)
        {
            await _wage_typeservice.Update(wage_Type);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _wage_typeservice.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
