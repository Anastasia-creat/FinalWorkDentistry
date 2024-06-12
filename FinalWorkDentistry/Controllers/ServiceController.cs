
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Mapster;
using FinalWorkDentistry.DataAccessLayer;
using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using FinalWorkDentistry.Models;
using static System.Reflection.Metadata.BlobBuilder;


namespace FinalWorkDentistry.Controllers
{
 
    public class ServiceController : Controller
    {

        ApplicationDbContext db;
        private int _serviceQuantityPerPage = 10;

        private readonly IRepository<Service> _repositoryServices;
       private readonly IRepository<CategoryService> _repositoryCategories;

        public ServiceController(IRepository<Service> repositoryServices,
            IRepository<CategoryService> repositoryCategories, ApplicationDbContext context)
        {
            _repositoryServices = repositoryServices;
            _repositoryCategories = repositoryCategories;
            db = context;
        }
      public IActionResult ListView(string categoryName, string searchString, int page = 1)
        {
            var category = _repositoryCategories
               .FindByName(categoryName);

            var query = _repositoryServices
               .GetList()
              .Where(p =>
        (category == null || p.ServiceCategory.CategoryServiceId == category.CategoryServiceId) &&
        (string.IsNullOrEmpty(searchString) || p.Name.Contains(searchString)));

            var servicesSample = query
                .OrderBy(x => x.ServiceId)
                .Skip((page - 1) * _serviceQuantityPerPage)
                .Take(_serviceQuantityPerPage)
                .Select(e => new ServicesBriefModel(e));

            int totalServicesQuantity = query.Count();

            int pagesQuantity = (int)
                Math.Ceiling(
                totalServicesQuantity /
                (double)_serviceQuantityPerPage
                );

            var model = new ServicePageModel
            {
                ServicesForPage = servicesSample,
                ActivePage = page,
                PagesQuantity = pagesQuantity,
                CategoryName = categoryName,
                SearchString = searchString

            };

            return View(model);
        }

        public IActionResult Search(string searchString)
        {
            // Получаем все книги из базы данных
            var service= _repositoryServices.GetList();

            // Если поисковая строка не пустая
            if (!string.IsNullOrEmpty(searchString))
            {
                // Фильтруем книги по названию, автору и жанру
                searchString = searchString.ToLower(); // Привести строку поиска к нижнему регистру для нечувствительности к регистру
                service = service.Where(b =>
                    b.Name.ToLower().Contains(searchString) ||
                    b.ServiceCategory.Name.ToLower().Contains(searchString)).ToList();
            }

            // Передаем отфильтрованные книги в представление для отображения
            return View(service);
        }


        public IActionResult ServicesDetailsView(long id)
        {
            var entity = _repositoryServices.Read(id);
           
            
            var model = entity.Adapt<ServicesModel>();

            return View(model);

          

        }

        [HttpPost]

        public IActionResult Create(RequestModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Request
                {
                    RequestId = (int)model.RequestId,
                    Name = model.Name,
                    Phone = model.Phone,
                    Symptoms = model.Symptoms
                };

                db.Requests.Add(entity);
                db.SaveChanges();

                return RedirectToAction("ListView");
            }

            // If model state is not valid, return to the form with the current model to show validation errors
            return View(model);
        }

      
    }

}

