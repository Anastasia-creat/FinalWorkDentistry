using FinalWorkDentistry.DataAccessLayer;
using FinalWorkDentistry.Domains;
using FinalWorkDentistry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkDentistry.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext db;

        public RequestController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RequestModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Request
                {
                    RequestId = (int)model.RequestId,
                    Name = model.Name,
                    Phone = model.Phone,
                    Symptoms = model.Symptoms,
                    Time = model.Time
                };

                db.Requests.Add(entity);
                await db.SaveChangesAsync();


                //  return RedirectToAction("Create");
                return Json(new { success = true });
            }

            // Возвращаем JSON-ответ с ошибкой
            return Json(new { success = false, message = "Ошибка при валидации данных." });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}


