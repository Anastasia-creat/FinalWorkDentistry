using Microsoft.AspNetCore.Mvc;
using FinalWorkDentistry.Models;
using System.Collections.Generic;
using System.Web.Mvc.Expressions;
//using System.Web.Mvc;

namespace KursovayaBlazorNet6.Controllers
{
    public class GoogleMapController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

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

            return  Json(googleMaps, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }
    }
}
