using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using API.Common;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    public class TokenController : ApiController
    {
        [HttpGet]
        [Route("auth/token/{userName}")]
        public IHttpActionResult GetToken(string userName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Cache.SecretKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var token = new JwtSecurityToken("Issuer", "Audience", claims, null, DateTime.Now.AddHours(1), signingCredentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}