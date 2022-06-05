using Domain.Modals.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _positionService.GetList());
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _positionService.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(PositionToAdd position)
        {
            await _positionService.Create(position);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(PositionToUpdate position)
        {
            await _positionService.Update(position);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _positionService.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
