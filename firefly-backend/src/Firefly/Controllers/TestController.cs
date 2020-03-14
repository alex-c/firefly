using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Firefly.Controllers
{
    [Route("test")]
    public class TestController : Controller
    {
        public TestController(ILogger<TestController> logger)
        {
            logger.LogInformation("test");
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
