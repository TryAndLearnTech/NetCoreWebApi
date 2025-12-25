using Microsoft.AspNetCore.Mvc;

namespace NetCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WelcomeController : Controller
    {
        [HttpGet(Name = "Welcome")]
        public string Welcome()
        {
            return "Hello, This is the test controller";
        }
    }
}
