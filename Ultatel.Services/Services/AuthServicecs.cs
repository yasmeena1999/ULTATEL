using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Entities;
using Ultatel.Core.Interfaces;

namespace Ultatel.Services.Services
{
    public class AuthServicecs : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthServicecs(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

       

            public  async Task<string> CreatTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim ("Name",user.FullName),
                new Claim ("Email",user.Email),
                new Claim ("UserId",user.Id),

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: authClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
