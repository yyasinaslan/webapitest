using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Context;
using Web.Dtos;
using Web.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly MyDbContext _db;

        public AuthController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Login(UserLoginDto credentials)
        {
            var products = await _db.Products.ToListAsync();

            return products;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public ActionResult<string> Register(UserLoginDto credentials)
        {
            return credentials.Username + '=' + credentials.Password;
        }

        [HttpDelete("DeleteAccount")]
        [Authorize]
        public ActionResult<string> DeleteMyAccount(UserLoginDto credentials)
        {
            return credentials.Username + '=' + credentials.Password;
        }


        private string CreateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(System.Configuration.ConfigurationManager.AppSettings["Jwt:Key"] ?? "defaultKey");

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),

                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                Expires = DateTime.UtcNow.AddMinutes(5),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
