using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Web.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost("Login")]
        public ActionResult<string> Login(UserLoginDto credentials)
        {
            return credentials.Username + '=' + credentials.Password;
        }

        [HttpPost("Register")]
        public ActionResult<string> Register(UserLoginDto credentials)
        {
            return credentials.Username + '=' + credentials.Password;
        }
    }
}
