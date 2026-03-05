using backend.Services.Cast;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCasts([FromServices] GetAllCast service) => await service.GetAll(HttpContext);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCastById([FromServices] GetCastById service) => await service.GetById(HttpContext);

        [HttpPost]
        public async Task<IActionResult> PostNewCast([FromServices] PostNewCast service) => await service.NewCast(HttpContext);

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCast([FromServices] DeleteCast service) => await service.Delete(HttpContext);
    }
}
