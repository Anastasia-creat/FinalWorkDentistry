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

        //public ActionResult GoogleMap()
        //{

        //    return View();
        //}

        public JsonResult GetData()
        {
            //создадим список данных
            List<GoogleMapsModel> googleMaps = new List<GoogleMapsModel>();

            googleMaps.Add(new GoogleMapsModel()
            {
                Id = 1,
                PlaceName = "Братьев Кашириных, 28",
                GeoLat = 55.174773,
                GeoLong = 61.393640,
                Clinic = "Жемчужина"
            });

            return Json(googleMaps, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

    }
}
