using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<List<Product>>> Login(UserLoginDto credentials)
        {
            var products = await _db.Products.ToListAsync();
            
            return products;
        }

        [HttpPost("Register")]
        public ActionResult<string> Register(UserLoginDto credentials)
        {
            return credentials.Username + '=' + credentials.Password;
        }
    }
}
