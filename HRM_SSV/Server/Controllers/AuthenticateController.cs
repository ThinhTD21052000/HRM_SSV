using AutoMapper;
using Domain.Modals.Response;
using Domain.Modals.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Entities;
using Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthenticateController(UserManager<User> userManager, IConfiguration configuration, IUserService userService, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                //create token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                //return token
                UserToGet userToGet = _mapper.Map<UserToGet>(user);
                userToGet.ImageFile = Convert.ToBase64String(userToGet.Avatar);
                userToGet.RoleName = userRoles[0];
                return Ok(
                    new Response()
                    {
                        User = userToGet,
                        IsSuccess = true,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                    });
            }
            return Ok(
                    new Response()
                    {
                        IsSuccess = false,
                    }); 
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserToAdd userToAdd)
        {
            var userExists = await _userManager.FindByNameAsync(userToAdd.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Lỗi", Message = "User đã tồn tại!" });
            var result = await _userService.Create(userToAdd);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Lỗi", Message = "Lỗi tạo tài khoản!" });
            return Ok(new Response { Status = "Thành công!", Message = "User được tạo thành công!" });

        }
    }
}
