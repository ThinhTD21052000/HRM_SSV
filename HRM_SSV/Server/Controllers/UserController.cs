using Domain.Modals.Response;
using Domain.Modals.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Entities;
using Server.Services;

namespace Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _userService.GetList());
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid Id)
        {
            return Ok(await _userService.Get(Id));
        }


        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(UserToAdd user)
        {
            return Ok(await _userService.Create(user));
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
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);
            return Ok("Delete Success!");
        }      

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordReponse reponse)
        {
            await _userService.ChangePassword(reponse.User, reponse.OldPassword, reponse.NewPassword);
            return Ok("Change Password Success!");
        }  
        
        [HttpGet]
        [Route("CheckPassword")]
        public async Task<IActionResult> CheckPassword(Guid id, string password)
        {
            return Ok(await _userService.CheckPassword(id, password));
        }        
    }
}
