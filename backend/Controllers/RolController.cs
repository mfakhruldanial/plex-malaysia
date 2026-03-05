using backend.Services.Rol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRoles([FromServices] GetAllRoles service) => await service.GetAll(HttpContext);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] GetRolById service) => await service.GetById(HttpContext);

        [HttpPost]
        public async Task<IActionResult> Create([FromServices] PostNewRol service) => await service.New(HttpContext);

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromServices] PutRol service) => await service.Edit(HttpContext);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromServices] DeleteRol service) => await service.Delete(HttpContext);
    }
}