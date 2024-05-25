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
//using System.Web.Providers.Entities;

namespace FinalWorkDentistry.Controllers
{
    public class ReviewController : Controller
    {
        //private static string filePath = "reviews.json";

        //[HttpPost]
        //public IActionResult AddReview([FromBody] Reviews review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        List<Reviews> reviews = GetAllReviewsFromJsonFile();
        //        reviews.Add(review);
        //       System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(reviews));
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        //[HttpGet]
        //public IActionResult GetAllReviews()
        //{
        //    var reviews = GetAllReviewsFromJsonFile();
        //    return Ok(reviews);
        //}

        //private List<Reviews> GetAllReviewsFromJsonFile()
        //{
        //    if (!System.IO.File.Exists(filePath))
        //        return new List<Reviews>();

        //    string json = System.IO.File.ReadAllText(filePath);
        //    return JsonConvert.DeserializeObject<List<Reviews>>(json);
        //}

        private readonly IRepository<Reviews> _reviewRepository; // Предполагается, что у вас есть репозиторий для работы с отзывами
        private readonly ApplicationDbContext _dbContext;

        public ReviewController(IRepository<Reviews> reviewRepository, ApplicationDbContext dbContext)
        {
            _reviewRepository = reviewRepository;
            _dbContext = dbContext;
        }


        public IActionResult ListView()
        {
          //  var category = _repositoryCategories.FindByName(categoryName);

            var query = _reviewRepository.GetList();

            var servicesSample = query.OrderBy(x => x.ReviewId)
                                //   .Take(_serviceQuantityPerPage)
                                   .Select(e => e.Adapt<ReviewBriefModel>());

          //  var totalServicesQuantity = query.Count();

            var model = new ReviewsModel
            {
                ReviewForPage = servicesSample
               
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitReview(ReviewsModel model)
        {
            if (ModelState.IsValid)
            {
                var review = new Reviews
                {
                    NameReview = model.NameReview,
                    Text = model.Text,
                 
                };

                _dbContext.Reviews.Add(review);
                await _dbContext.SaveChangesAsync();


                // Сериализуем отзыв в JSON
                string json = JsonConvert.SerializeObject(review);

                // Задаем путь к файлу JSON
                string filePath = "ReviewsJson/Отзывы.json";

                // Проверяем существование файла
                if (!System.IO.File.Exists(filePath))
                {
                    // Если файл не существует, создаем его и записываем JSON
                    System.IO.File.WriteAllText(filePath, "[" + json + "]");
                }
                else
                {
                    // Если файл существует, добавляем новый отзыв в существующий JSON массив
                    string jsonFromFile = System.IO.File.ReadAllText(filePath);
                    jsonFromFile = jsonFromFile.Substring(0, jsonFromFile.Length - 1); // Удаляем последний символ (закрывающую скобку массива)
                    jsonFromFile += "," + json + "]"; // Добавляем новый отзыв в массив
                    System.IO.File.WriteAllText(filePath, jsonFromFile); // Записываем обновленный массив в файл
                }


                return RedirectToAction("ListView"); // Перенаправляем на страницу с отзывами
            }

            return View(model); // Возвращаем форму с сообщениями об ошибках
        }


        //[HttpPost]
        //public IActionResult AddReview([FromBody] Reviews review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _reviewRepository.Create(review);
        //        return Ok(); // Возвращаем успешный статус HTTP
        //    }
        //    return BadRequest(); // Возвращаем статус HTTP "ошибка запроса" в случае неверных данных
        //}


        //public async Task<IActionResult> Index()
        //{
        //    return View(_reviewRepository.GetList());
        //}
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(Reviews review)
        //{

        //    _reviewRepository.Create(review);

        //    _reviewRepository.Update(review);
        //    return RedirectToAction("Index");
        //}


        //--------

        //[HttpGet]
        //public IActionResult ReviewsView()
        //{
        //    return View();

        //}

        //[HttpPost]
        //public IActionResult ReviewsView(string name, string text, bool isAgree)
        //{
        //    string authData = $"Name: {name}   Text: {text}   isAgree: {isAgree}";

        //    if(authData != null)
        //    {

        //    }
        //    return RedirectToAction("Reviews");
        //}

        //public IActionResult Reviews()
        //{
        //    return View();
        //}


        //public IActionResult Index()
        //{
        //    return View();
        //}

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
