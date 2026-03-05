using backend.Services.UserApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromServices] GetAllUsers service) => await service.GetUsers(HttpContext);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromServices] GetUserById service) => await service.GetById(HttpContext);

        [HttpPost]
        public async Task<IActionResult> PostNewUser([FromServices] PostNewUser service) => await service.NewUser(HttpContext);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromServices] PutUser service) => await service.EditUser(HttpContext);
    }
}