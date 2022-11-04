using Domain.Modals.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _userService.GetList());
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int Id)
        {
            return Ok(await _userService.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(UserToAdd user)
        {
            await _userService.Create(user);
            return Ok("Insert Success!");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UserToUpdate user)
        {
            await _userService.Update(user);
            return Ok("Update Success!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok("Delete Success!");
        }        
    }
}
