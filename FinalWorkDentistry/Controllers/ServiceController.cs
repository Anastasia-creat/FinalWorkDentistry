
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

       

        public IActionResult ListView(string categoryName, int page = 1)
        {
            var category = _repositoryCategories
               .FindByName(categoryName);

            var query = _repositoryServices
               .GetList()
               .Where(p =>
                   category == null ||
                   p.ServiceCategory.CategoryServiceId == category.CategoryServiceId);

            // добавляем механизм пагинации
            var servicesSample = query
                .OrderBy(x => x.ServiceId)
                .Skip((page - 1) * _serviceQuantityPerPage)
                .Take(_serviceQuantityPerPage)
                .Select(e => new ServicesBriefModel(e));
                //.Select(e => e.Adapt<ServicesBriefModel>(e));

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
                CategoryName = categoryName

            };

          

            return View(model);




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

        //private readonly string accountSid = "AC13908ea809d7898a34918c3cdf83b5fd";
        //private readonly string authToken = "d0f4cbe218c57b33dee08bb159ddd415";
        //private readonly string fromPhoneNumber = "+18148502119";

        //[HttpPost("send-confirmation-sms")]
        //public async Task<IActionResult> SendConfirmationSmsAsync([FromBody] SmsRequestModel request)
        //{
        //    try
        //    {
        //        TwilioClient.Init(accountSid, authToken);

        //        var message = await MessageResource.CreateAsync(
        //            body: $"Your confirmation code is: {request.ConfirmationCode}",
        //            from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
        //            to: new Twilio.Types.PhoneNumber(request.ToPhoneNumber)
        //        );

        //        return Ok(new { MessageSid = message.Sid });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Error = ex.Message });
        //    }
        //}
    }

}

