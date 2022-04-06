using Microsoft.AspNetCore.Mvc;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hello()
        {
            return this.Ok("Hello");
        }
    }
}
