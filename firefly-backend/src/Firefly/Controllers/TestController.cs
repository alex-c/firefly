using Microsoft.AspNetCore.Mvc;

namespace Firefly.Controllers
{
    [Route("test")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
