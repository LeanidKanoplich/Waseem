using Microsoft.AspNetCore.Mvc;

namespace SolarpayAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("API is working!");
        }
    }
}
