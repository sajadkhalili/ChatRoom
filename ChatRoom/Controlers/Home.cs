using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controlers
{
    [Authorize]
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
