using FinalWorkDentistry.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalWorkDentistry.Controllers
{
    public class BlazorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

     


        public IActionResult IndexMain()
        {
            return View();
        }



        public IActionResult Questions()
        {
            return View();
        }

    }
}
