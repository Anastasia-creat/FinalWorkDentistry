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

                return RedirectToAction("ListView"); 
            }

            return View(model); 
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
