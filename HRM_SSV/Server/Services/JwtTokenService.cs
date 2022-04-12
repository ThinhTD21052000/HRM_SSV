//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Yakureki.Server.Token
//{
//    public interface IJwtTokenService
//    {
//        string BuildToken(UserInfo userInfo);
//    }

//    public class JwtTokenService : IJwtTokenService
//    {
//        private readonly IConfiguration _config;


//        public JwtTokenService(IConfiguration config)
//        {
//            _config = config;
//        }

//        public string BuildToken(UserInfo userInfo)
//        {
//            var claims = new List<Claim>();
//            claims.Add(new Claim(ClaimTypes.Role, userInfo.Password));
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
//                _config["Jwt:Audience"],
//                claims,
//                expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:ExpireTime"])),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
