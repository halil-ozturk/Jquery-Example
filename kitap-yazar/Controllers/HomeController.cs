using Microsoft.AspNetCore.Mvc;

namespace kitap_yazar.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
