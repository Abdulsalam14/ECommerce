using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {

            HttpContext.Session.SetInt32("Age", 24);
            HttpContext.Session.SetString("Name", "Elvin");
            return Ok();
        }

        public string Get()
        {
            var name=HttpContext.Session.GetString("Name");
            var age = HttpContext.Session.GetInt32("Age");
            return $"{name}  {age}";
        }
    }
}
