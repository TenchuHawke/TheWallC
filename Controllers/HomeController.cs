using Microsoft.AspNetCore.Mvc;

namespace TheWall.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            return View ();
        }
    }
}