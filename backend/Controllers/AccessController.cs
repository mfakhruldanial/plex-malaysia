using backend.Services.Access;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Auth([FromServices] Login service) => await service.Auth(HttpContext);

        [HttpPost("Signup")]
        public async Task<IActionResult> Register([FromServices] Signup service) => await service.Register(HttpContext);
    }
}