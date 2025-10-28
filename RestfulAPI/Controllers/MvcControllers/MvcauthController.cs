using Microsoft.AspNetCore.Mvc;

namespace RestfulAPI.Controllers.MvcControllers
{
    public class MvcauthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // Views/MvcAuth/Login.cshtml
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Views/MvcAuth/Register.cshtml
        }
    }
}
