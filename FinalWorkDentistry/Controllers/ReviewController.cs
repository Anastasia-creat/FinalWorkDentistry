using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.DataAccessLayer;
using FinalWorkDentistry.Domains;
using FinalWorkDentistry.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalWorkDentistry.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IRepository<Reviews> _reviewRepository; // Предполагается, что у вас есть репозиторий для работы с отзывами
        private readonly ApplicationDbContext _dbContext;

        public ReviewController(IRepository<Reviews> reviewRepository, ApplicationDbContext dbContext)
        {
            _reviewRepository = reviewRepository;
            _dbContext = dbContext;
        }

        public IActionResult ListView()
        {
            var query = _reviewRepository.GetList();
            var servicesSample = query.OrderBy(x => x.ReviewId)
                                      .Select(e => e.Adapt<ReviewBriefModel>())
                                      .ToList();

            var model = new ReviewsModel
            {
                ReviewForPage = servicesSample
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(ReviewBriefModel model)
        {
            if (ModelState.IsValid)
            {
                var review = new Reviews
                {
                    NameReview = model.NameReview,
                    Text = model.Text
                };

                _dbContext.Reviews.Add(review);
                await _dbContext.SaveChangesAsync(); // Сохраняем изменения в базе данных

                // Сохранение в JSON файл
                string json = JsonConvert.SerializeObject(review);
                string filePath = "ReviewsJson/Отзывы.json";

                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.WriteAllText(filePath, "[" + json + "]");
                }
                else
                {
                    string jsonFromFile = System.IO.File.ReadAllText(filePath);
                    jsonFromFile = jsonFromFile.Substring(0, jsonFromFile.Length - 1);
                    jsonFromFile += "," + json + "]";
                    System.IO.File.WriteAllText(filePath, jsonFromFile);
                }

                return RedirectToAction("ListView"); // Перенаправляем на ListView для обновления списка отзывов
            }

            return View(model); // Возвращаем представление с моделью в случае ошибки
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
