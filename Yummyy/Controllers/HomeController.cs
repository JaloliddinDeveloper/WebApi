using Microsoft.AspNetCore.Mvc;

namespace Yummyy.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()=>
         View();
    }
}
